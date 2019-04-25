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
        private string _UserName;
        private string _userPassowrd;

        public DelegateCommand CreateNewUserCommand => new DelegateCommand(CreateNewUser);
        public DelegateCommand CheckForLoginCommand => new DelegateCommand(CheckForLogin);

        public string UserName
        {
            get { return _UserName; }
            set
            {
                SetProperty(ref _UserName, value);
            }
        }

        public string UserPassword
        {
            get { return _userPassowrd; }
            set
            {
                SetProperty(ref _userPassowrd, value);
            }
        }

        private void CreateNewUser()
        {
            //Cause of the passwordBox this has to be done very complicated
            UserPassword = PasswordBoxAssistant.PasswordContent;
            PasswordBoxAssistant.PasswordContent.Remove(0, PasswordBoxAssistant.PasswordContent.Length);

            //Here a new User will be created with a new instance of the User class
            var Id = Guid.NewGuid();
            var newUser = new User(UserName, UserPassword, Id, DateTime.Now);
            DataAccess.Instance.RegisterUser(newUser);
        }

        private void CheckForLogin()
        {
            DataAccess.Instance.ReadFromXml("Users", UserName);
        }
    }
}
