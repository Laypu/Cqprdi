using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class EPaperViewModel
    {
            public int? group { get; set; }
            public string itemid { get; set; }
            public string mid { get; set; }
            public int nowpage { get; set; }

            public string title { get; set; }
            public string fromDate { get; set; }
            public string toDate { get; set; }
            public string classType { get; set; }

            public string Introduction { get; set; }

            public bool ClassOverview { get; set; }
            public List<EPaperItem> ListEPaperItem { get; set; }
            public List<GroupEPaper> ListGroupEPaper { get; set; }

            public EPaperItem EPaperItem { get; set; }
            /// <summary>
            /// 內頁
            /// </summary>

            public string GroupName { get; set; }

            public List<ColumnSetting> columnSettings { get; set; }


    }


}
