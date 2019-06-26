using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.BaseDataModules;

namespace TTracker.GUI.Models
{
    class UtilityViewModel<T> : BindableBase
    {
        /// <summary>
        /// This class takes in a ViewModel and is used to represent their Properties, but it also displays aditional Properties
        /// which you only need in a very specific situation. Basically stuff that you only need once or twice and that is not worth
        /// adding to the original VM
        /// </summary>

        private T _currentViewModel;
        private ViewModelManagementBase _currentBase;
        private bool _isSelected;

        //Selected Command when you work with buttons in an itemscontrol
        public DelegateCommand SelectedCommand => new DelegateCommand(Selected);

        public T CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                SetProperty(ref _currentViewModel, value);
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
                _currentBase.SelectedVm = CurrentViewModel;
            }
        }

        public UtilityViewModel(object viewModel, ViewModelManagementBase currentBase)
        {
            CurrentViewModel = (T)viewModel;
            _currentBase = currentBase;
        }

        void Selected()
        {
            IsSelected = true;
        }
    }
}
