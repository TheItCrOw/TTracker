using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.Utility;

namespace TTracker.GUI.ViewModels.Entities
{
    class MicroTaskViewModel : ViewModelBase
    {

        /// <summary>
        /// This Vm doesnt implement the isDirty logic, it instantly saves when the propery is changed
        /// </summary>
        #region Properties
        private string _text;
        private bool _isChecked;

        public MicroTask Model { get; set; }

        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value);
                ChangedProperties.Add(nameof(Text));
                Save();
            }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                SetProperty(ref _isChecked, value);
                ChangedProperties.Add(nameof(IsChecked));
                Save();
            }
        }

        #endregion

        public DelegateCommand DeleteCommand => new DelegateCommand(Delete);

        public MicroTaskViewModel(MicroTask microTask, ViewModelManagementBase currentBase)
        {
            Model = microTask;
            CurrentBase = currentBase;
            CreateViewModel();
            AfterSave();
        }

        private void CreateViewModel()
        {
            Id = Guid.NewGuid();
            Text = Model.Text;
            ModelId = Model.Id;

            switch (Model.IsChecked)
            {
                case 0:
                    IsChecked = false;
                    break;
                case 1:
                    IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        void Save()
        {
            var changedPropertiesFullData = new List<string>();

            foreach (var p in ChangedProperties)
            {
                switch (p)
                {
                    case "Text":
                        Model.Text = this.Text;
                        changedPropertiesFullData.Add(("Text/" + Model.Text));
                        break;
                    case "IsChecked":
                        if (IsChecked)
                        {
                            Model.IsChecked = 1;
                        }
                        else
                        {
                            Model.IsChecked = 0;
                        }
                        changedPropertiesFullData.Add("IsChecked/" + Model.IsChecked);
                        break;
                }
            }
            DataAccess.Instance.Save<MicroTask>(this.Model, changedPropertiesFullData);
        }

        void Delete()
        {
            ((HomeViewModel)CurrentBase).MicroTasks.Remove(this);
            DataAccess.Instance.DeleteEntity<MicroTask>(Model);
        }
    }
}
