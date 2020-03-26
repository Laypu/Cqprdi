using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class ADKanban
    {
        [ExplicitKey]
        public int ID { get; set; }
        [ExplicitKey]
        public int? Lang_ID { get; set; }

        public string Type_ID { get; set; }

        public string AD_Name { get; set; }

        public string Img_Show_Name { get; set; }

        public string Img_Name_Ori { get; set; }

        public string Img_Name_Thumb { get; set; }

        public string Link_Href { get; set; }

        public string Link_Mode { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? UpdateDatetime { get; set; }

        public string UpdateUser { get; set; }

        public string ADDesc { get; set; }

        public string VideoLink { get; set; }
        #region 影片
        public string UploadVideoFileName { get; set; }
        public string UploadVideoFilePath { get; set; }
        public string UploadVideoFileDesc { get; set; }

        #endregion

    }


}
