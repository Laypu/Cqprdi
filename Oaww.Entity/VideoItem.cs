using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class VideoItem : ItemBase
    {

        public int? Lang_ID { get; set; }
        public string Title { get; set; }
        public int? ClickCnt { get; set; }
        public int? Link_Mode { get; set; }
        public string HtmlContent { get; set; }

        public string UploadFileDesc { get; set; }

        public string ImageFileOrgName { get; set; }
        public string ImageFileDesc { get; set; }
        public string ImageFileLocation { get; set; }
        public string LinkUrl { get; set; }
        public bool? Enabled { get; set; }

        public DateTime? StDate { get; set; }

        public DateTime? EdDate { get; set; }
        public DateTime? PublicshDate { get; set; }
        public DateTime? UpdateDatetime { get; set; }

        public int? ActiveID { get; set; }

        public int? ActiveItemID { get; set; }

        public string RelateImageFileName { get; set; }
        public string RelateImageFileOrgName { get; set; }
        public string Introduction { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public string CreateName { get; set; }
        public string UpdateName { get; set; }
        public bool? IsVerift { get; set; }
        public string LinkUrlDesc { get; set; }
        public string VideoLink { get; set; }

        [Write(false)]
        public string GroupName { get; set; }

        [Write(false)]
        public string No { get; set; }

    }
}
