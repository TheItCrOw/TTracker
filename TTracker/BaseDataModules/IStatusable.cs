using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.BaseDataModules
{
    interface IStatusable
    {
        Status Status { get; set; }

    }

    public enum Status
    {
        [Description("Static")]
        Static,
        [Description("Todo")]
        Todo,
        [Description("Working")]
        Working,
        [Description("Finished")]
        Finished,
        [Description("Blocked")]
        Blocked,
    }
}
