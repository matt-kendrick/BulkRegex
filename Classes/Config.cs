using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkRegEx.Classes
{
    internal class Config
    {
        public string Description { get; set; }
        public bool CreateNewFiles { get; set; }
        public string OutputPath { get; set; }
        public MatchReplace[] MatchReplaces { get; set; }
    }
}
