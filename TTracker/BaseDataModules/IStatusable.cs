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
        [Description("Working")]
        Working,
        [Description("Todo")]
        Todo,
        [Description("Blocked")]
        Blocked,
        [Description("Finished")]
        Finished,

    }
}
