using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class ADMain
    {
        [Key]
        public int ID { get; set; }
        public int? Lang_ID { get; set; }
        public int? Sort { get; set; }
        public string Type_ID { get; set; }
        public DateTime? StDate { get; set; }
        public DateTime? EdDate { get; set; }
        public string AD_Name { get; set; }
        public string ADDesc { get; set; }
        public string Icon { get; set; }

        public string Color { get; set; }
        public string Img_Name_Ori { get; set; }
        public string Img_Name_Thumb { get; set; }
        public string Img_Show_Name { get; set; }
        public string Img_Name_Ori2 { get; set; }
        public string Img_Show_Name2 { get; set; }
        public string Img_Name_Thumb2 { get; set; }
        public int? AD_Width { get; set; }
        public int? AD_Height { get; set; }
        public string Link_Href { get; set; }
        public string Link_Mode { get; set; }


        public bool? Enabled { get; set; }
        public DateTime? Create_Date { get; set; }
        public DateTime? UpdateDatetime { get; set; }
        public string UpdateUser { get; set; }

        public string SType { get; set; }

        public bool? IsVerift { get; set; }

        #region 影片
        public string UploadVideoFileName { get; set; }
        public string UploadVideoFilePath { get; set; }
        public string UploadVideoFileDesc { get; set; }
       
        #endregion
    }
}
