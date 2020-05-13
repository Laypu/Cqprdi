using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public partial class EPaperItem : ItemBase
    {
        //public int ItemID { get; set; }
        public int Lang_ID { get; set; }

        //因為已經繼承itembase 所以註解
        //public int? Sort { get; set; }

        //public int? ModelID { get; set; }
        //public int GroupID { get; set; }
        public Nullable<int> PaperMode { get; set; }
        public Nullable<int> PaperStyle { get; set; }
        public string PublishStr { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string PageEndHtmlContent { get; set; }
        public string TopBannerImgPath { get; set; }
        public string TopBannerImgOrgName { get; set; }
        public string TopBannerImgName { get; set; }
        public string TopHtmlContent { get; set; }
        public string LeftHtmlContent { get; set; }
        public string CenterHtmlContent { get; set; }
        public string BottomHtmlContent { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<bool> Enabled { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }


        
    }
}
