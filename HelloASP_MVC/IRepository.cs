using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloASP_MVC
{
    public interface IRepository
    {
        string GetId(string name);
    }
}