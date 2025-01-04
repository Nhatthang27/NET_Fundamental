using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFind
{
    public class FileLineSource : ILineSource
    {
        private string _path;
        private StreamReader? _reader;
        private int _lineNumber = 0;

        public FileLineSource(string path)
        {
            _path = path;
        }

        public void Close()
        {
            if (_reader != null)
            {
                _reader.Close();
                _reader = null;
            }
        }

        public void Open()
        {
            _reader = new StreamReader(new FileStream(_path, FileMode.Open, FileAccess.Read));
        }

        public Line? ReadLine()
        {
            if (_reader == null)
            {
                throw new InvalidOperationException("FileLineSource is not open");
            }
            var s = _reader.ReadLine();
            if (s == null) return null;
            else return new Line { LineNumber = ++_lineNumber, Text = s };
        }
    }
}