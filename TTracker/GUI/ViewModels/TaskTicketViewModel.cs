using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TTracker.BaseDataModules;
using TTracker.GUI.Models;
using TTracker.GUI.ViewModels.TicketManagementSubVms;

namespace TTracker.GUI.ViewModels
{
    public class TaskTicketViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _name;
        private string _text;
        private DateTime _created;
        private string _projectName;
        private Guid _Id;
        private float _expectedTime;
        private float _usedTime;
        private string _progress;

        public Guid Id { get { return _Id; } set { SetProperty(ref _Id, value); } }
        public string ProjectName { get { return _projectName; } set { SetProperty(ref _projectName, value); } }
        public DateTime Created { get { return _created; } set { SetProperty(ref _created, value); } }
        public float UsedTime { get { return _usedTime; } set { SetProperty(ref _usedTime, value); } }
        public string Progress { get { return _progress; } set { SetProperty(ref _progress, value); } }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
                SetIsDirty();
                InformBaseViewModel();
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                SetProperty(ref _text, value);
                SetIsDirty();
                InformBaseViewModel();
            }
        }
        public float ExpectedTime
        {
            get
            {
                return _expectedTime;
            }
            set
            {
                SetProperty(ref _expectedTime, value);
                SetIsDirty();
                InformBaseViewModel();
            }
        }

        public TaskTicketViewModel(TaskTicket taskTicket, ViewModelManagementBase currentBase)
        {
            CurrentBase = currentBase;

            Name = taskTicket.Name;
            Id = Guid.NewGuid();
            ProjectName = taskTicket.ProjectName;
            Text = taskTicket.Text;
            Created = DateTime.Now;
            ExpectedTime = taskTicket.ExpectedTime;
            UsedTime = taskTicket.UsedTime;
            Progress = UsedTime + " / " + ExpectedTime + " Days";

            AfterSave();
        }

        public void Save()
        {
            if (!isDirty)
                return;

            MessageBox.Show(this.Name + " has been saved");

            AfterSave();
        }




    }
}
