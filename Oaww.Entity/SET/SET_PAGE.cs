using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity.SET
{
    public class SET_PAGE
    {
        public int ID { get; set; }
        public string M_PAGE01 { get; set; }
        /// <summary>
        /// folder
        /// </summary>
        public string M_PAGE02 { get; set; }
        /// <summary>
        /// ModelID
        /// </summary>
        public int? M_PAGE03 { get; set; }
        /// <summary>
        /// 照片是否Multi
        /// </summary>
        public bool? M_PAGE04 { get; set; }

    }
}
