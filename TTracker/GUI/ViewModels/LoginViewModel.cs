using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TTracker.GUI.Models;
using TTracker.GUI.Views;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class LoginViewModel : BindableBase
    {
        private string _newUserName;
        private string _newUserPassword;

        public DelegateCommand CreateNewUserCommand => new DelegateCommand(CreateNewUser);

        public string NewUserName
        {
            get { return _newUserName; }
            set
            {
                SetProperty(ref _newUserName, value);
            }
        }

        public string NewUserPassword
        {
            get { return _newUserPassword; }
            set
            {
                SetProperty(ref _newUserPassword, value);
            }
        }

        private void CreateNewUser()
        {
            //Cause of the passwordBox this has to be done very complicated
            NewUserPassword = PasswordBoxAssistant.PasswordContent;
            PasswordBoxAssistant.PasswordContent = "";

            //Here a new User will be created with a new instance of the User class
            var Id = Guid.NewGuid();
            var newUser = new User(NewUserName, NewUserPassword, Id, DateTime.Now);
            DataAccess.Instance.AddNewUser(newUser);

        }
    }
}
