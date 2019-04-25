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
        private User SaveableUser;

        private DataAccess()
        {

        }

        public void RegisterUser(Object newUser)
        {
            var _newUser = (User)newUser;
            SaveableUser = _newUser;
            SaveUser();
            //Clear the password out of the memory
            _newUser.Password.Remove(0, PasswordBoxAssistant.PasswordContent.Length);
            PasswordBoxAssistant.PasswordContent.Remove(0, PasswordBoxAssistant.PasswordContent.Length);
        }

        //This writes an xml file of the user
        public void SaveUser()
        {
            Directory.CreateDirectory(_saveDataPath + "Users");

            string xmlPath = _saveDataPath + "Users\\";
            string xmlName = SaveableUser.Id.ToString() + ".xml";
            string fullLocationPath = xmlPath + xmlName;

            using (XmlWriter writer = XmlWriter.Create(fullLocationPath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("User");

                writer.WriteElementString("ID", SaveableUser.Id.ToString());
                writer.WriteElementString("Name", SaveableUser.Name);
                writer.WriteElementString("Password", SaveableUser.Password);
                writer.WriteElementString("Created", SaveableUser.Created.ToString());

                writer.WriteEndElement();
                writer.WriteEndDocument();

            }

        }

    }
}
