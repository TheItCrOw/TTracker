using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class AccountViewModel : BindableBase, INotifyPropertyChanged
    {
        private string _currentUserName;

        public AccountViewModel()
        {
            SetLoggedInUserProperties();
        }

        public string CurrentUserName
        {
            get { return _currentUserName; }
            set
            {
                SetProperty(ref _currentUserName, value);
            }
        }

        private void SetLoggedInUserProperties()
        {
            if (DataAccess.CurrentLoggedUser == null)
            {
                CurrentUserName = "You are not logged in";
                return;
            }
            CurrentUserName = ($" You are currently logged in as: {DataAccess.CurrentLoggedUser.Name}");
        }


    }
}
