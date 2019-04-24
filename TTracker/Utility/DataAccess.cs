using System;
using System.Collections;
using System.Collections.Generic;
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

        //Keeps track of all the registeredUsers
        private List<User> SaveableUsers = new List<User>();

        private DataAccess()
        {

        }

        public void RegisterUser(Object newUser)
        {
            var _newUser = (User)newUser;
            SaveableUsers.Clear();
            SaveableUsers.Add(_newUser);
            SaveUser();
            //Clear the password out of the memory
            _newUser.Password = "";
        }

        //This write into a xml file well, but the location must be set right
        public void SaveUser()
        {
            foreach (var user in SaveableUsers)
            {
                using (XmlWriter writer = XmlWriter.Create(user.Name.ToString() + ".xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("User");

                    writer.WriteElementString("ID", user.Id.ToString());
                    writer.WriteElementString("Name", user.Name);
                    writer.WriteElementString("Password", user.Password);
                    writer.WriteElementString("Created", user.Created.ToString());

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }

        }

    }
}
