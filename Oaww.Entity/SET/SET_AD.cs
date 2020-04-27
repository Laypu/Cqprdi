using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity.SET
{
    public class SET_AD
    {
        [Key]
        public int ID { get; set; }

        public string M_AD01 { get; set; }
        public string M_AD02 { get; set; }
        public bool M_AD03 { get; set; }
        public bool M_AD04 { get; set; }
        public string M_AD05 { get; set; }
        public bool M_AD06 { get; set; }
        public bool M_AD07 { get; set; }

        public bool M_AD08 { get; set; }
        public bool M_AD09 { get; set; }
        public string M_AD10 { get; set; }
        public string Lang_ID { get; set; }
        public bool M_AD011 { get; set; }
        /// <summary>
        /// Index 新增
        /// </summary>
        public bool M_AD12 { get; set; }
        /// <summary>
        /// Index 刪除
        /// </summary>
        public bool M_AD13 { get; set; }
        /// <summary>
        /// Index 顯示區間
        /// </summary>
        public bool M_AD14 { get; set; }
    }
}
