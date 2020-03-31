using Oaww.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.Business
{
    class SiteConfigService : BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();

        public SiteConfig GetALLSiteConfig(string ID)
        {
            return _commonService.GetALLSiteConfig(ID);
        }
    }
}
