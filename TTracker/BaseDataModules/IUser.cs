using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTracker.Interfaces
{
    public interface IUser
    {
        DateTime Created { get; set; }
        string Password { get; set; }

    }
}
