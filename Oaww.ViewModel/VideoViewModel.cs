using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class VideoViewModel
    {
        public int group { get; set; }
        public string itemid { get; set; }
        public string mid { get; set; }
        public int nowpage { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string title { get; set; }
        public int ShowCount { get; set; }

        public List<VideoItem> ListVideoItem { get; set; }
        public List<GroupVideo> ListGroupVideo { get; set; }
    }
}
