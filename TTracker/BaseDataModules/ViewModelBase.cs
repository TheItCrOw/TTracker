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

        public bool IsDirty { get; set; }
        public bool IsNew { get; set; }
        public ViewModelManagementBase CurrentBase { get; set; }

        public List<string> ChangedProperties = new List<string>();

        private Guid _Id;
        public Guid Id { get { return _Id; } set { SetProperty(ref _Id, value); } }

        protected void SetIsDirty(string PropertyName)
        {
            IsDirty = true;

            if (!ChangedProperties.Contains(PropertyName))
                ChangedProperties.Add(PropertyName);

            InformBaseViewModel();
        }

        protected void AfterSave()
        {
            IsDirty = false;
            IsNew = false;
            CurrentBase.HasUnsavedChanges = false;
            ChangedProperties.Clear();
        }

        protected void InformBaseViewModel()
        {
            CurrentBase.HasUnsavedChanges = true;
        }

    }
}
