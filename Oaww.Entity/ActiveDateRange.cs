using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Web;

namespace Oaww.Entity
{
    public class ActiveDateRange
    {
        [Key]
        public int ActID { get; set; }

        public int ItemID { get; set; }

        public int ModelID { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
