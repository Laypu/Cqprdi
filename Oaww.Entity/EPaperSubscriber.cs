using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public partial class EPaperSubscriber
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string CHNName { get; set; }
        public string EMail { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> DisableDate { get; set; }
        public string UpdateUser { get; set; }
        public string OPDateStr { get; set; }
    }
}
