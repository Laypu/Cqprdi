using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Web;

namespace Oaww.ViewModel
{
    public class SiteLayoutModel
    {
        public SiteLayoutModel()
        {
            ID = -1;
        }
        public int? ID { get; set; }
        public string LangID { get; set; }
        public string LogoImgNameOri { get; set; }
        public string LogoImgShowName { get; set; }
        public string LogoImgNameThumb { get; set; }
        public string LogoImageUrl { get; set; }
        public string LogoImageUrlThumb { get; set; }

        public string InnerLogoImgNameOri { get; set; }
        public string InnerLogoImgShowName { get; set; }
        public string InnerLogoImgNameThumb { get; set; }
        public string InnerLogoImageUrl { get; set; }
        public string InnerLogoImageUrlThumb { get; set; }



        public string FirstPageImgNameOri { get; set; }
        public string FirstPageImgShowName { get; set; }
        public string FirstPageImgNameThumb { get; set; }
        public string FirstPageImageUrl { get; set; }
        public string InsidePageImgNameOri { get; set; }
        public string InsidePageImgShowName { get; set; }
        public string InsidePageImgNameThumb { get; set; }
        public string InsidePageImageUrl { get; set; }
        [JsonIgnore]
     
        public HttpPostedFileBase LogoImageFile { get; set; }
        [JsonIgnore]
      
        public HttpPostedFileBase InnerLogoImageFile { get; set; }
        [JsonIgnore]
       
        public HttpPostedFileBase FirstPageImageFile { get; set; }
        [JsonIgnore]
       
        public HttpPostedFileBase InsidePageImageFile { get; set; }
        [JsonIgnore]
       
        public HttpPostedFileBase FowardImageFile { get; set; }
        [JsonIgnore]
       
        public HttpPostedFileBase PrintImageFile { get; set; }
        public string HtmlContent { get; set; }

        public string FowardImgNameOri { get; set; }
        public string FowardImgShowName { get; set; }
        public string FowardImgNameThumb { get; set; }
        public string FowardImageUrl { get; set; }
        public string FowardHtmlContent { get; set; }

        public string PrintImgNameOri { get; set; }
        public string PrintImgShowName { get; set; }
        public string PrintImgNameThumb { get; set; }
        public string PrintImageUrl { get; set; }
        public string PrintHtmlContent { get; set; }

        public string Page404HtmlContent { get; set; }
        public string Page404Title { get; set; }

        public string SType { get; set; }
        public string PublishContent { get; set; }

        public string Recommand { get; set; }
        public string RecommandImgNameOri { get; set; }
        public string RecommandImgShowName { get; set; }
        public string RecommandImageUrl { get; set; }
        [JsonIgnore]

        public HttpPostedFileBase RecommandImageFile { get; set; }
    }
}
