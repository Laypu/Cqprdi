using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity.SiteMap;
using Oaww.Entity.SET;

namespace Oaww.ViewModel.SiteMap
{
    public class SiteMapEditModel
    {
        public SiteMapItem SiteMapItem { get; set; }

        public List<SiteMapKey> siteMapKeys { get; set; }       

        public string ModelName { get; set; }
        
        public SET_BASE SET_BASE { get; set; }
    }
}
