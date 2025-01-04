using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFind
{
    public class LineSourceFactory
    {
        public static ILineSource[] CreateInstance(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new ILineSource[] { new ConsoleLineSource() };
            }
            else
            {
                var dir = new DirectoryInfo(path);
                if (dir.Exists)
                {
                    var files = dir.GetFiles();
                    return files.Select(f => new FileLineSource(f.FullName)).ToArray();
                }
            }
            return [];
        }
    }
}