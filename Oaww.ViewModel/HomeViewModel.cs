using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class HomeViewModel
    {
        public int NowLanguage { get; set; }
        public List<ADMain> ListMainRoller { get; set; }
        public List<ADMain> ListIcon { get; set; }
        public ADMain Marquee { get; set; }
        /// <summary>
        /// 主題專區
        /// </summary>
        public ADKanban ThemeKanban { get; set; }
        /// <summary>
        /// 快速連結專區
        /// </summary>
        public List<ADMain> QuickLink { get; set; }
        /// <summary>
        /// 主題專區列表
        /// </summary>
        public List<ADMain> ListThemeAD { get; set; }
        /// <summary>
        /// 本會介紹
        /// </summary>
        public ADKanban Introduction { get; set; }
        /// <summary>
        /// 動態消息
        /// </summary>
        public ADKanban DynamicNews { get; set; }

        public List<MessageItem> AboutLink { get; set; }
        public List<MessageItem> ListFaq { get; set; }
        public List<MessageItem> ListNews { get; set; }
        public List<MessageItem> ListImage { get; set; }
        public List<MessageItem> ListDownload { get; set; }

        public List<ADMain> ListMainMobileRoller { get; set; }
    }
}
