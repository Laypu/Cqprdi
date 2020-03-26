using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dapper.Contrib.Extensions;
using Oaww.Entity;
using Oaww.Entity.SET;

namespace Oaww.ViewModel
{
    public class ADEditModel
    {
        public ADEditModel()
        {
            ID = -1;
            SET_AD = new SET_AD();
            SET_ADKanban = new SET_ADKanban();

        }
        [Key]
        public int ID { get; set; }
        public int Lang_ID { get; set; }
        public int Sort { get; set; }
        public string Type_ID { get; set; }
        public DateTime? StDate { get; set; }
        public DateTime? EdDate { get; set; }
        public string AD_Name { get; set; }

        public string ADDesc { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        ///桌面照片
        public string Img_Show_Name { get; set; }
        public string Img_Name_Ori { get; set; }
        public string Img_Name_Thumb { get; set; }
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string Img_Show_Name2 { get; set; }
        public string Img_Name_Ori2 { get; set; }
        public string Img_Name_Thumb2 { get; set; }
        public string ImageUrl2 { get; set; }
        public HttpPostedFileBase ImageFile2 { get; set; }
        public int? AD_Width { get; set; }
        public int? AD_Height { get; set; }


        //手機照片
        public string UploadVideoFileName { get; set; }
        public string UploadVideoFilePath { get; set; }
        public string UploadVideoFileDesc { get; set; }
      

       
        public HttpPostedFileBase VideoFile { get; set; }
      


        /// <summary>
        /// P:電腦/M:手機
        /// </summary>
        public string SType { get; set; }
        public string Link_Href { get; set; }
        public string Link_Mode { get; set; }

        public string StDateStr { get; set; }
        public string EdDateStr { get; set; }

        /// <summary>
        /// main=>主要
        /// </summary>
        public string Type { get; set; }

        //public bool Fixed { get; set; }

        //
        //public String UseFileName { get; set; }
        //

        public VerifyData VerifyData { get; set; }

        //public string ADDesc { get; set; }
        public string VideoLink { get; set; }
        public SET_AD SET_AD { get; set; }
        public SET_ADKanban SET_ADKanban { get; set; }
    }
}
