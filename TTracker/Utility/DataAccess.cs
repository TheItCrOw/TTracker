using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TTracker.GUI.Models;

namespace TTracker.Utility
{
    public sealed class DataAccess
    {
        private static readonly Lazy<DataAccess>
            lazy =
            new Lazy<DataAccess>
            (() => new DataAccess());

        public static DataAccess Instance { get { return lazy.Value; } }

        private string _saveDataPath = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\";

        //Keeps track of all the registeredUsers
        private List<string> SaveableDataList = new List<string>();

        private DataAccess()
        {

        }

        public void RegisterUser(Object newUser)
        {
            var _newUser = (User)newUser;
            SaveableDataList.Clear();
            SaveableDataList.Add("Id/" + _newUser.Id);
            SaveableDataList.Add("Name/" + _newUser.Name);
            SaveableDataList.Add("Password/" + _newUser.Password);
            SaveableDataList.Add("Created/" + _newUser.Created.ToString());
            SaveToXml("Users", _newUser.Id, SaveableDataList);
            //Clear the password out of memory
            _newUser.Password.Remove(0, PasswordBoxAssistant.PasswordContent.Length);
            PasswordBoxAssistant.PasswordContent.Remove(0, PasswordBoxAssistant.PasswordContent.Length);
        }

        /// <summary>
        /// Writes the data that is given into the list into a xml file. 
        /// Split like this: Name/paul
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="data"></param>
        public void SaveToXml(string directoryName, Guid ID, List<string> data)
        {
            Directory.CreateDirectory(_saveDataPath + directoryName);
            string xmlPath = _saveDataPath + directoryName + "\\";
            string xmlName = ID.ToString() + ".xml";
            string fullLocationPath = xmlPath + xmlName;

            using (XmlWriter writer = XmlWriter.Create(fullLocationPath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Data");

                foreach (var s in data)
                {
                    string[] splitedString = s.Split(new char[] { '/' });
                    string dataName = splitedString[0];
                    string dataValue = splitedString[1];

                    writer.WriteElementString(dataName, dataValue);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

            }

        }

    }
}
