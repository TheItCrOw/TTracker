using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.GUI.Views;

namespace TTracker.GUI.ViewModels
{
    class HomeViewModel : BindableBase
    {
        private string _date;

        public HomeViewModel()
        {
            Date = ($"Your To-Do´s for today, the {DateTime.Now.ToShortDateString()}");    
        }

        public string Date
        {
            get { return _date; }
            set
            {
                SetProperty(ref _date, value);
            }
        }

    }
}
