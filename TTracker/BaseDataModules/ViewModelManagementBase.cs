using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.BaseDataModules
{
    public class ViewModelManagementBase : BindableBase
    {

        private bool _hasUnsavedChanges;
        private object _currentContent;

        public bool HasUnsavedChanges
        {
            get
            {
                return _hasUnsavedChanges;
            }
            set
            {
                SetProperty(ref _hasUnsavedChanges, value);
            }
        }

        //This always has the curretn View Content
        public object CurrentContent
        {
            get
            {
                return _currentContent;
            }
            set
            {
                SetProperty(ref _currentContent, value);
            }
        }
    }
}
