using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFind
{
    public class FilterLineSource : ILineSource
    {
        private ILineSource _parent;
        private Func<Line, bool> _filter;

        public FilterLineSource(ILineSource parent, Func<Line, bool> filter)
        {
            _parent = parent;
            _filter = filter;
        }
        public void Close()
        {
            _parent.Close();
        }

        public void Open()
        {
            _parent.Open();
        }

        public Line? ReadLine()
        {
            while (true)
            {
                var line = _parent.ReadLine();
                if (line == null) return null;
                if (_filter(line)) return line;
            }

        }
    }
}