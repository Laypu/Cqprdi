using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public partial class EPaperUnitSetting:UnitSetting
    {
        public Nullable<int> LangID { get; set; }

        public string FrontPagePath { get; set; }
        //public int ID { get; set; }

        //public Nullable<bool> IsRSS { get; set; }

        //public Nullable<int> ShowCount { get; set; }
        //public string Column1 { get; set; }
        //public string Column2 { get; set; }
        //public string Column3 { get; set; }
        //public string Column4 { get; set; }
        //public string Column5 { get; set; }
        //public string Column6 { get; set; }
        //public string Column7 { get; set; }
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
    }
}
