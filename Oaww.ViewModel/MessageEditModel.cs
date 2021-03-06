﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Oaww.Entity;
using Oaww.Entity.SET;

namespace Oaww.ViewModel
{
    public class MessageEditModel
    {
        public MessageEditModel()
        {
            ItemID = -1;
            PublicshStr = DateTime.Now.ToString("yyyy/MM/dd");
            Title = "";
            messageImages = new List<MessageImage>();
            messageDateRange = new List<ActiveDateRange>();
        }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public int ItemID { get; set; }
        public int Group_ID { get; set; }

        public DateTime? StDate { get; set; }
        public DateTime? EdDate { get; set; }
        public string StDateStr { get; set; }
        public string EdDateStr { get; set; }
        public string PublicshStr { get; set; }
        public string UnPublicshStr { get; set; }
        public string Title { get; set; }
        public int? Link_Mode { get; set; }
        public string HtmlContent { get; set; }
        public string LinkUrl { get; set; }
        public string ActiveID { get; set; }
        public string ActiveItemID { get; set; }

        public string ImageUrl { get; set; }
        public string ImageFileDesc { get; set; }
        public string ImageFileLocation { get; set; }
        [JsonIgnore]
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageFileOrgName { get; set; }
        public string ImageFileName { get; set; }


        public string RelateImagelUrl { get; set; }
        [JsonIgnore]
        public HttpPostedFileBase RelateImageFile { get; set; }
        public string RelateImageFileOrgName { get; set; }
        public string RelateImageName { get; set; }


        public string UploadFileName { get; set; }
        public string UploadFilePath { get; set; }
        public string UploadFileDesc { get; set; }
        public int? UploadFileSize { get; set; }
        public string UploadFileType { get; set; }
        [JsonIgnore]
        public HttpPostedFileBase UploadFile { get; set; }

        public string WebsiteTitle { get; set; }
        public string Description { get; set; }
        public string[] Keywords { get; set; }
        public string Introduction { get; set; }
        public string VerifyStatus { get; set; }
        public string VerifyUser { get; set; }
        public string VerifyDateTime { get; set; }
        public string VerifyDept { get; set; }

        public string CreateUser { get; set; }
        public string CreateDatetime { get; set; }
        public string CreateDept { get; set; }
        public string UpdateDatetime { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateDept { get; set; }
        public string LinkUrlDesc { get; set; }
        public string OpenMode { get; set; }
        public bool Home { get; set; }
        public string ComnonHtml1 { get; set; }
        public string ComnonHtml2 { get; set; }
        public List<MessageFile> fileDownloadFiles { get; set; }
        public  List<MessageImage> messageImages { get; set; }
        public List<ActiveDateRange> messageDateRange { get; set; }
        public SET_MESSAGE SET_MESSAGE { get; set; }
        public SET_BASE SET_BASE { get; set; }
    }
}
