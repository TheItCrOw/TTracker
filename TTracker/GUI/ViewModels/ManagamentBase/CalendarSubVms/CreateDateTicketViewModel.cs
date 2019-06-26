using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.GUI.ViewModels.ManagamentBase.CalendarSubVms
{
    public class CreateDateTicketViewModel
    {
        private CalendarViewModel _currentBase;
        public CreateDateTicketViewModel(CalendarViewModel currentBase)
        {
            _currentBase = currentBase;
        }
    }
}
