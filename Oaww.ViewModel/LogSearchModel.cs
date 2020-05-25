using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel
{
    public class LogSearchModel : SearchModelBase
    {
        public string Operation { get; set; }
        public string sdate { get; set; }
        public string edate { get; set; }
        public string GroupID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Trx { get; set; }
        public string KeyWord { get; set; }
    }

    public class LogData
    {
        public DateTime LM_Time { get; set; }
        public string Operation { get; set; }
        public string MUser { get; set; }
        public string ModelName { get; set; }
        public string ItemName { get; set; }
        public string Name { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
    }
}
