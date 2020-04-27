using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity.SET
{
    public class SET_EPAPER
    {
        public int ID { get; set; }
        /// <summary>
        /// 2:電子報模組
        /// </summary>
        public int? M_EPAPER01 { get; set; }
        /// <summary>
        /// Folder
        /// </summary>
        public string M_EPAPER02 { get; set; }
        /// <summary>
        /// 照片是否Multi
        /// </summary>
        public bool M_EPAPER03 { get; set; }
        /// <summary>
        /// 檔案是否Multi
        /// </summary>
        public bool M_EPAPER04 { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string M_EPAPER05 { get; set; }
        /// <summary>
        /// Controller Name
        /// </summary>
        public string M_EPAPER06 { get; set; }
        /// <summary>
        /// 列表圖片是否顯示
        /// </summary>
        public bool M_EPAPER07 { get; set; }
        /// <summary>
        /// 開啟格式設定是否顯示
        /// </summary>
        public bool M_EPAPER08 { get; set; }
        /// <summary>
        /// 內容
        /// </summary>
        public bool M_EPAPER09 { get; set; }
        /// <summary>
        /// 單一圖片顯示
        /// </summary>
        public bool M_EPAPER10 { get; set; }
        /// <summary>
        /// 相關連結
        /// </summary>
        public bool M_EPAPER11 { get; set; }

        public bool M_EPAPER12 { get; set; }
        public string M_EPAPER13 { get; set; }
        public string M_EPAPER14 { get; set; }
        public bool M_EPAPER15 { get; set; }
        public bool M_EPAPER16 { get; set; }
        public bool M_EPAPER17 { get; set; }
        public string M_EPAPER18 { get; set; }
        /// <summary>
        /// 活動區間
        /// </summary>
        public bool M_EPAPER19 { get; set; }
    }
}
