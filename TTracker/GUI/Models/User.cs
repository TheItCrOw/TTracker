using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.Interfaces;
using TTracker.Utility;

namespace TTracker.GUI.Models
{
    public class User : IUser
    {

        private DateTime _created;
        private string _password;
        private Guid _id;
        private string _name;

        public DateTime Created { get { return _created; } set => _created = value; }
        public string Password { get { return _password; } set => _password = value; }
        public Guid Id { get { return _id; } set => _id = value; }
        public string Name { get { return _name; } set => _name = value; }


        public User(string name, string password, Guid id, DateTime created)
        {
            Name = name;
            Password = password;
            Id = id;
            Created = created;
        }

    }
}
