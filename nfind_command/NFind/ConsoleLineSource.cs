using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFind
{
    public class ConsoleLineSource : ILineSource
    {
        private int _lineNumber = 0;

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public Line? ReadLine()
        {
            var line = Console.ReadLine();
            if (line == null) return null;
            else return new Line { LineNumber = ++_lineNumber, Text = line };
        }
    }
}