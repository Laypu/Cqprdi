using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel
{
    public class SearchModelBase
    {        
        public string Search { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int NowPage { get; set; }
        public string Key { get; set; }
        public string LangId { get; set; }
        public int ModelID { get; set; }

        public int cnt { get; set; }
    }
}
