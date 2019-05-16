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

        public void SetIsDirty()
        {
            isDirty = true;
        }

    }
}
