using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class PageIndexSetting
    {
        [Key]
        public int ID { get; set; }
        public int? Lang_ID { get; set; }
        public int? ShowCount { get; set; }
        public string HtmlContent { get; set; }

    }
}
