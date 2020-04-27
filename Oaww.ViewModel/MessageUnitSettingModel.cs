using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel
{
    public class MessageUnitSettingModel : UnitSettingModelBase
    {
        public MessageUnitSettingModel()
        {
            ID = -1;
            ShowCount = 10;
            UnitSettingColumnList = new List<UnitSettingColumn>();
        }
        



    }
}
