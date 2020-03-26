using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel.Search
{
    public class ConfigSearchModel : SearchModelBase
    {
        public string ModuleName { get; set; }
        public string PublicshDateFrom { get; set; }
        public string PublicshDateTo { get; set; }
        public string Empolyee { get; set; }
        public string Title { get; set; }
        public string ChangeStatus { get; set; }
        public string VerifyStatus { get; set; }

        public ConfigSearchModel()
        {
            Search = string.Empty;
            Sort = "ID";
            Order = "desc";
            Limit = 10;
            Offset = 1;
            NowPage = 1;
            Key = "";
            LangId = "1";
            ModelID = -1;
        }
    }
}
