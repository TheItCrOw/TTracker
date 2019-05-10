using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTracker.Interfaces;

namespace TTracker.GUI.Models
{
    class Ticket : IBaseData
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public float ExpectedTime { get; set; }
        public float UsedTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime PlannedDate { get; set; }
    }
}
