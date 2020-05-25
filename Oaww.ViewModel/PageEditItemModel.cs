using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Oaww.Entity.SET;
using Oaww.Entity;
using Newtonsoft.Json;

namespace Oaww.ViewModel
{
    public class PageEditItemModel
    {
        public PageEditItemModel()
        {
            ItemID = -1;
        }
        public int? ItemID { get; set; }
        public int? ModelID { get; set; }
        public string ModelName { get; set; }
        public string UploadFileName { get; set; }
        public string UploadFilePath { get; set; }
        public string UploadFileDesc { get; set; }
        [JsonIgnore]
        public HttpPostedFileBase UploadFile { get; set; }
        public string LinkUrl { get; set; }


        [JsonIgnore]
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageFileOrgName { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileDesc { get; set; }

        public string ImageFileLocation { get; set; }
        
        public string WebsiteTitle { get; set; }
        public string Description { get; set; }
        public string[] Keywords { get; set; }
        public string ImageUrl { get; set; }

        public string HtmlContent { get; set; }
       

        public string ActiveID { get; set; }
        public string ActiveItemID { get; set; }
        public string LangID { get; set; }

        public string LinkUrlDesc { get; set; }

        public string VerifyUser { get; set; }
        public string VerifyDateTime { get; set; }
        public string VerifyStatus { get; set; }
        public string VerifyDept { get; set; }
        public string CreateUser { get; set; }
        public string CreateDept { get; set; }
        public string CreateDatetime { get; set; }
        public string UpdateDatetime { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateDept { get; set; }

        public SET_BASE SET_BASE { get; set; }
        public SET_PAGE SET_PAGE { get; set; }
        public List<PageImage> PageImages { get; set; }
    }
}
