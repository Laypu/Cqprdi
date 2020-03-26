using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class FileDownloadEditModel
    {
        public FileDownloadItem fileDownloadItem { get; set; }

        public List<FileDownloadFile> fileDownloadFiles { get; set; }


        public VerifyData VerifyData { get; set; }

        public bool? Status { get; set; }
    }
}
