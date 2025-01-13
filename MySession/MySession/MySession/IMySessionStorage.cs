using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySession.MySession
{
    public interface IMySessionStorage
    {
        ISession Create();
        ISession Get(string id);
    }
}