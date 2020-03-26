using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class ItemBase
    {
        [Key]
        public int ItemID { get; set; }
        public int? ModelID { get; set; }

        [Write(false)]
        public string ModelName { get; set; }

        public int? GroupID { get; set; }
        public int? Sort { get; set; }
        public string UploadFileName { get; set; }
        public string UploadFilePath { get; set; }
        public string ImageFileName { get; set; }
    }
}
