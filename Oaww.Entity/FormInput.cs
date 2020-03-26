using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class FormInput
    {
        [Key]
        public int ID { get; set; }

        public int? MainID { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string JSONStr { get; set; }

        public DateTime? CreateDatetime { get; set; }

        public string Progress { get; set; }

        public string ProcessAccount { get; set; }

        public DateTime? ProcessDatetime { get; set; }

        public string ReplyAccount { get; set; }

        public DateTime? ReplyDatetime { get; set; }

       
        [Write(false)]
        public string Title { get; set; }

    }
}
