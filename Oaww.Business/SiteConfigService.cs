using Oaww.Entity;
using Oaww.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.Business
{
   public class SiteConfigService : BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();

        public SiteConfig GetALLSiteConfig(string ID)
        {
            return _commonService.GetALLSiteConfig(ID);
        }

        public bool Update(SiteConfig SiteConfig)
        {
            try
            {
                return base.UpdateObject(SiteConfig);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "SiteConfig更新異常,error:" + ex.ToString().NewLineReplace());

                return false;
            }

        }
    }
}
