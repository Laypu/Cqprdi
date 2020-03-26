using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.Utility;

namespace Oaww.Business
{
    public class SEOService:Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonSevice = new CommonService();

        public SEO GetSEO(string TypeName, string Lang_ID)
        {
            return _commonSevice.GetGeneral<SEO>("TypeName=@TypeName and Lang_ID=@Lang_ID"
                                                     , new Dictionary<string, string>() { { "TypeName", TypeName }, { "Lang_ID", Lang_ID } });
        }

        public int Create(SEO SEO)
        {
            try
            {
                return (int)base.InsertObject(SEO);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "SEO新增異常,error:" + ex.ToString().NewLineReplace());

                return 0;
            }

        }

        public bool Update(SEO SEO)
        {
            try
            {
                return base.UpdateObject(SEO);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "SEO更新異常,error:" + ex.ToString().NewLineReplace());

                return false;
            }

        }
    }
}
