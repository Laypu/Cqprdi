using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class MessageViewModel
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
        public List<MessageItem> ListMessageItem { get; set; }
        public List<GroupMessage> ListGroupMessage { get; set; }

        public MessageItem MessageItem { get; set; }
        /// <summary>
        /// 內頁
        /// </summary>

        public string GroupName { get; set; }

        public List<ColumnSetting> columnSettings { get; set; }

        public List<MessageFile> ListMessageFile { get; set; }
        public List<MessageImage> ListMessageImage { get; set; }

    }
}
