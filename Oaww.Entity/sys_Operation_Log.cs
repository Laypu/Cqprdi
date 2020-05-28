using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.Entity
{
   public class sys_Operation_Log
    {
        [Key]
        public int PK { get; set; }
        public string Operation { get; set; }
        public string Operator { get; set; }
        public string OperationModelName { get; set; }
        public string OperationItemName { get; set; }
        public string TrxType { get; set; }
        public string After { get; set; }
        public string Before { get; set; }
        public DateTime LM_Time { get; set; }
    }
}
