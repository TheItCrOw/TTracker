using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.BaseDataModules
{
    public class ModelBase
    {

        private Guid _id;
        public Guid Id { get { return _id; } set => _id = value; }


    }
}
