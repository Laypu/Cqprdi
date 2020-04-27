using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel
{
    
        public class EPaperItemResult
        {
            public int ItemID { get; set; }
            public string Title { get; set; }
            public string FormatStr { get; set; }
            public string IsEditStr { get; set; }
            public bool? Enabled { get; set; }
            public int Sort { get; set; }
            public bool? IsPublished { get; set; }
            public string PublishStr { get; set; }
            public string Introduction { get; set; }
            public string IsPublishStr { get; set; }
            
            public string GroupName { get; set; }

            
    }
    
}
