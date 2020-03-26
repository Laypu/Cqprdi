﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.Entity
{
    public class LangKey
    {
        public int ID { get; set; }

        public string GroupName { get; set; }

        public int? SubGroup { get; set; }

        public string Item { get; set; }

        public int? Sort { get; set; }

        public string LKey { get; set; }

        public bool? Used { get; set; }

    }
}
