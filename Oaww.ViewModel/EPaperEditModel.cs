using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Oaww.Entity;
using Oaww.Entity.SET;

namespace Oaww.ViewModel
{
    public class EPaperEditModel
    {
        public EPaperEditModel() {
            ItemID = -1;
            PublishStr = DateTime.Now.ToString("yyyy/MM/dd");
        }
        

        public int ItemID { get; set; }

        public int Lang_ID { get; set; }
        public int ModelID { get; set; }
        public int PaperMode { get; set; }

        public int PaperStyle { get; set; }

        public string PublishStr { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
      
        public string TopBannerImgUrl { get; set; }
        public string TopBannerImgPath { get; set; }
        public string TopBannerImgOrgName { get; set; }
        public string TopBannerImgName { get; set; }
        public HttpPostedFileBase TopBannerImg { get; set; }

        public string PageEndHtmlContent { get; set; }
        public string TopHtmlContent { get; set; }
        public string LeftHtmlContent { get; set; }
        public string CenterHtmlContent { get; set; }
        public string BottomHtmlContent { get; set; }

        public string UploadFileName { get; set; }
        public string UploadFilePath { get; set; }
        public string ImageFileName { get; set; }


        public int GroupID { get; set; }
        //public HttpPostedFileBase[] ADImageFiles { get; set; }
        //public string[] ADName { get; set; }
        //public string[] ADLink { get; set; }
        //public string[] ADID { get; set; }
        //public string[] ADFileName { get; set; }
        //public string[] ADFilePath { get; set; }
        //public string EPaperContent { get; set; }
        public List<EPaperItemEdit> EPaperItemEdit { get; set; }

        public bool Enabled { get; set; }

        public SET_EPAPER SET_EPAPER { get; set; }

        public SET_BASE SET_BASE { get; set; }
    }
    public class EPaperItemEdit
    {

        public string Name { get; set; }
        public string MenuID { get; set; }
        public string MainID { get; set; }
        public int SortID { get; set; }
        public List<string> ItemName { get; set; }
        public List<string> ItemUrl { get; set; }
        public List<string> ItemKey { get; set; }
        
    }
 }



