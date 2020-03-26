using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class DownloadViewModel
    {
        public string itemid { get; set; }
        public string mid { get; set; }
        public int? group { get; set; }

        public int? group2 { get; set; }

        public string Remark { get; set; }

        public string title { get; set; }

        public List<GroupFileDownload> ListGroupMessage { get; set; }

        public List<GroupFileDownload> ListGroupMessage2 { get; set; }

        public List<FileDownloadItem> ListFileDownloadItem { get; set; }


        public List<ColumnSetting> columnSettings { get; set; }
    }
}
