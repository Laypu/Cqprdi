﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.ViewModel
{
    public class SEOViewModel
    {
        public SEOViewModel()
        {
            ID = -1;
        }
        [Key]
        public int ID { get; set; }
        public string WebsiteTitle { get; set; }
        public string Description { get; set; }
        public string[] Keywords { get; set; }
        public string TypeName { get; set; }
        public string TypeID { get; set; }
        public string Lang_ID { get; set; }
    }
}
