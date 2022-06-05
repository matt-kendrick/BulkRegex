using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkRegEx.Classes
{
    internal class MatchReplace
    {
        public string Match { set; get; }
        public string Replace { set; get; }
        public bool IgnoreCase { set; get; }
        public bool MultiLine { set; get; }
    }
}
