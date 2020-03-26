using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.Utility;

namespace Oaww.Business
{
    public class ModelService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();

        public PageIndexSetting GetPageIndexSetting(string ID)
        {
            return _commonService.GetGeneral<PageIndexSetting>("Lang_ID=@Lang_ID"
                                                                    , new Dictionary<string, string> { { "Lang_ID", ID } });
        }

        public string SetPageIndexSettingModel(PageIndexSetting model, string langid, string account)
        {
            try
            {
                model.Lang_ID = int.Parse(langid);
                var r = 0;
                if (model.ID <= 0)
                {
                    r = (int)base.InsertObject(model);
                }
                else
                {
                    r = base.UpdateObject(model) ? 1 : 0;
                }

                if (r > 0)
                {
                    return "設定成功";
                }
                else
                {
                    return "設定失敗";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "設定全文檢索異常,error:" + ex.ToString().NewLineReplace());
                return "設定失敗";
            }
        }
    }
}
