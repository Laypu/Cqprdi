using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.Entity
{
    public class SiteConfig
    {
        [Key]
        public int ID { get; set; }
        public int? TotalVisitCnt { get; set; }
        public string LastUpdateDate { get; set; }
    }
}
