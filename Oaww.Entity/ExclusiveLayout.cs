using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Web;

namespace Oaww.Entity
{
    public class ExclusiveLayout
    {
        public int ID { get; set; }
        public int? GroupID { get; set; }

        [Write(false)]
        public string GROUP_NAME { get; set; }

        public int? Manager { get; set; }
        [Write(false)]
        public string ManagerName { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Remark { get; set; }
        public bool? Enabled { get; set; }
        public int Lang_ID { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileOrgName { get; set; }
        [Write(false)]
        public string ImagelUrl { get; set; }
        [Write(false)]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
