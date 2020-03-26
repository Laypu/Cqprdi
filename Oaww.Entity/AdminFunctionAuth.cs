using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class AdminFunctionAuth
    {
        [ExplicitKey]
        public string GroupID { get; set; }

        [ExplicitKey]
        public int LangID { get; set; }

        [ExplicitKey]
        public int ItemID { get; set; }

        [ExplicitKey]
        public int Type { get; set; }

        public int GID { get; set; }

        public string FunGroup { get; set; }

        [Write(false)]
        public int? MenuType { get; set; }

    }
}
