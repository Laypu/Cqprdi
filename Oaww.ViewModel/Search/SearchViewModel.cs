using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel.Search
{
    public class SearchViewModel
    {
        public string ModuleName { get; set; }
        public int ModelID { get; set; }
        public int ItemID { get; set; }
        public string URL { get; set; }
        public int ModuleID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MenuType { get; set; }
        public string MenuLevel { get; set; }
        public int ID { get; set; }
        public int ParentID { get; set; }

        public DateTime CreateDatetime { get; set; }
    }
}
