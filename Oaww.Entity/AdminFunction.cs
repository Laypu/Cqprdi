using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class AdminFunction
    {
        [Key]
        public int ID { get; set; }
        public int? GroupID { get; set; }
        public string ItemName { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Parameter { get; set; }
        public int? ParentGroupID { get; set; }
    }
}
