using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity.SiteMap
{
    public class SiteMapItem : ItemBase
    {

        public int Lang_ID { get; set; }

        public string Title { get; set; }

        public string HtmlContent { get; set; }

        public string ImageFileOrgName { get; set; }

        public string ImageFileDesc { get; set; }

        public string ImageFileLocation { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? UpdateDatetime { get; set; }

        public string UpdateUser { get; set; }

        public string UpdateName { get; set; }

        [Write(false)]
        public string UpdateDept { get; set; }

        public DateTime? CreateDatetime { get; set; }

        public string CreateUser { get; set; }

        public string CreateName { get; set; }

      
    }
}
