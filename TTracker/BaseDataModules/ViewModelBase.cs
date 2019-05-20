using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.BaseDataModules
{
    public class ViewModelBase : BindableBase
    {

        public bool isDirty;

        public ViewModelManagementBase CurrentBase;

        public List<string> ChangedProperties = new List<string>();

        protected void SetIsDirty(string PropertyName)
        {
            isDirty = true;

            if (!ChangedProperties.Contains(PropertyName))
                ChangedProperties.Add(PropertyName);

            InformBaseViewModel();
        }

        protected void AfterSave()
        {
            isDirty = false;
            CurrentBase.HasUnsavedChanges = false;
            ChangedProperties.Clear();
        }

        protected void InformBaseViewModel()
        {
            CurrentBase.HasUnsavedChanges = true;
        }


    }
}
