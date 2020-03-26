using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity.SiteMap
{
    public class SiteMapKey
    {
        [Key]
        public int SID { get; set; }

        public int ModelID { get; set; }

        public string AreaName { get; set; }

        public string QuickKey { get; set; }

        public string Introduction { get; set; }
    }
}
