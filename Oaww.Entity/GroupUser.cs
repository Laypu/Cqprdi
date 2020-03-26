﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class GroupUser
    {
        [ExplicitKey]
        public string ID { get; set; }
        public int? Site_ID { get; set; }
        public int? Sort { get; set; }
        public string Group_Name { get; set; }
        public bool? Seo_Manage { get; set; }
        public bool? Readonly { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? UpdateDatetime { get; set; }
        public string UpdateUser { get; set; }
    }
}
