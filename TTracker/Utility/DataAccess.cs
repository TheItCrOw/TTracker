using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private List<User> RegisteredUser = new List<User>();

        private DataAccess()
        {

        }

        public void AddNewUser(Object newUser)
        {
            RegisteredUser.Add((User)newUser);
        }

    }
}
