using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.BaseDataModules
{
    interface IPrioritable
    {
        PriorityLevel Priority { get; set; }

    }

    public enum PriorityLevel
    {
        [Description("VeryLow")]
        VeryLow,
        [Description("Low")]
        Low,
        [Description("Normal")]
        Normal,
        [Description("High")]
        High,
        [Description("Very High")]
        VeryHigh,
    }
}
