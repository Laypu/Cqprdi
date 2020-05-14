using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel
{
    public class EPaperUnitSettingModel:UnitSettingModelBase
    {
        public EPaperUnitSettingModel()
        {
            ID = -1;
            ShowCount = 10;
            UnitSettingColumnList = new List<UnitSettingColumn>();

        }
        public string Column8 { get; set; }
        public string Column9 { get; set; }
        public string Column10 { get; set; }
        public string Column11 { get; set; }
        public string Column12 { get; set; }
        public string Column13 { get; set; }
        public string Column14 { get; set; }
        public string Column15 { get; set; }
        public string Column16 { get; set; }
        public string Column17 { get; set; }
        public string Column18 { get; set; }
        public string Column19 { get; set; }
        public string Column20 { get; set; }
        //public Nullable<System.DateTime> UpdateDatetime { get; set; }
        //public string UpdateUser { get; set; }
        //public string ColumnSetting { get; set; }
        public string Summary { get; set; }
        public string FrontPagePath { get; set; }

    }
}
