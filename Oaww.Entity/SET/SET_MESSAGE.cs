using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity.SET
{
    public class SET_MESSAGE
    {
        public int ID { get; set; }
        /// <summary>
        /// 2:訊息模組
        /// </summary>
        public int? M_MESSAGE01 { get; set; }
        /// <summary>
        /// Folder
        /// </summary>
        public string M_MESSAGE02 { get; set; }
        /// <summary>
        /// 照片是否Multi
        /// </summary>
        public bool M_MESSAGE03 { get; set; }
        /// <summary>
        /// 檔案是否Multi
        /// </summary>
        public bool M_MESSAGE04 { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string M_MESSAGE05 { get; set; }
        /// <summary>
        /// Controller Name
        /// </summary>
        public string M_MESSAGE06 { get; set; }
        /// <summary>
        /// 列表圖片是否顯示
        /// </summary>
        public bool M_MESSAGE07 { get; set; }
        /// <summary>
        /// 開啟格式設定是否顯示
        /// </summary>
        public bool M_MESSAGE08 { get; set; }
        /// <summary>
        /// 內容
        /// </summary>
        public bool M_MESSAGE09 { get; set; }
        /// <summary>
        /// 單一圖片顯示
        /// </summary>
        public bool M_MESSAGE10 { get; set; }
        /// <summary>
        /// 相關連結
        /// </summary>
        public bool M_MESSAGE11 { get; set; }

        public bool M_MESSAGE12 { get; set; }
        public string M_MESSAGE13 { get; set; }
        public string M_MESSAGE14 { get; set; }
        public bool M_MESSAGE15 { get; set; }
        public bool M_MESSAGE16 { get; set; }
        public bool M_MESSAGE17 { get; set; }
        public string M_MESSAGE18 { get; set; }
        /// <summary>
        /// 活動區間
        /// </summary>
        public bool M_MESSAGE19 { get; set; }
    }
}
