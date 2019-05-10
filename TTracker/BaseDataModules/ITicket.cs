using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.Interfaces;

namespace TTracker.GUI.Models
{
    interface ITicket : IBaseData
    {
        Guid UserId { get; set; }
        string Text { get; set; }
        DateTime Created { get; set; }
    }
}
