using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class SiteFlow
    {
        [Key]
        public int ID { get; set; }
        public int? Site_ID { get; set; }
        public string Siteflow_Code { get; set; }
        public string Siteflow_Link { get; set; }
        public string Siteflow_Code_Y { get; set; }
        public string Siteflow_Link_Y { get; set; }
    }
}
