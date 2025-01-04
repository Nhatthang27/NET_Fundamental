using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFind
{
    public class FindOption
    {
        public string StringToFind { get; set; } = string.Empty;
        public bool IsCaseSensitive { get; set; } = true;
        public bool FindDontContain { get; set; } = false;
        public bool CountMode { get; set; } = false;
        public bool ShowLineNumbers { get; set; } = false;
        public string Path { get; set; } = string.Empty;
        public bool HelpMode { get; set; } = false;
    }
}