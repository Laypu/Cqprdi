using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class ColumnSetting
    {
        [ExplicitKey]
        public int MainID { get; set; }

        [ExplicitKey]
        public string ColumnKey { get; set; }

        [ExplicitKey]
        public string Type { get; set; }

        public string ColumnName { get; set; }

        public int? Sort { get; set; }
        public bool? Used { get; set; }

    }
}
