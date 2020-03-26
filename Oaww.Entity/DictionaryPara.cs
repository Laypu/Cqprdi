using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity
{
    public class DictionaryPara
    {
        [ExplicitKey]
        public string ParaValue { get; set; }
        public string ParaName { get; set; }
        public string ParentValue { get; set; }
    }
}
