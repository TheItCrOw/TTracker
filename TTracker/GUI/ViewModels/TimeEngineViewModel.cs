using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels
{
    class TimeEngineViewModel : ViewModelManagementBase
    {
        private string _introText;

        public string IntroText
        {
            get { return _introText; }
            set
            {
                SetProperty(ref _introText, value);
            }
        }


        public TimeEngineViewModel()
        {
            GetCurrentUser();
        }

        void GetCurrentUser()
        {
            IntroText = "Hello " + DataAccess.CurrentLoggedUser.Name + "! What are you gonna do today, on the " + DateTime.Now.ToShortDateString() + "?";
        }

    }
}
