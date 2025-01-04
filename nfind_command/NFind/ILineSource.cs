using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFind
{
    public interface ILineSource
    {
        Line? ReadLine();
        void Open();
        void Close();
    }
}