using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.Utility
{
    public class ChartHelperClass
    {
        public string Name { get; set; }
        public float Share { get; set; }

        public ChartHelperClass(string name, float share)
        {
            Name = name;
            Share = share;
        }

    }
}
