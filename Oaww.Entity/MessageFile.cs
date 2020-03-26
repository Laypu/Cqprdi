using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class MessageFile : FileBase
    {
        [Key]
        public int FID { get; set; }

        public int ItemID { get; set; }
        public int ModelID { get; set; }
        public string UploadFileName { get; set; }


        public string UploadFilePath { get; set; }
        public string UploadFileDesc { get; set; }
        public string Title { get; set; }
        public int UploadFileSize { get; set; }
        public string UploadFileType { get; set; }

        /// <summary>
        /// file entity
        /// </summary>
        [Write(false)]

        public HttpPostedFileBase UploadFile { get; set; }

        /// <summary>
        /// actual Url
        /// </summary>
        [Write(false)]

        public string UploadFileUrl { get; set; }

    }
}
