using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class Dictionary
    {
        public int ID { get; set; }

        public string Category { get; set; }

        public int? Sort { get; set; }

        public string Group_Name { get; set; }

        public bool? Readonly { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? UpdateDatetime { get; set; }

        public string UpdateUser { get; set; }

        public int? ParentID { get; set; }
        [Write(false)]
        public string ParentName { get; set; }
        public int? ExCode { get; set; }

        [Write(false)]
        public string IsInsert { get; set; }

        [Write(false)]
        public string Type { get; set; }
    }
}
