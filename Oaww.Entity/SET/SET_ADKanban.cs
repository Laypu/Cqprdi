using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity.SET
{
    public class SET_ADKanban
    {
        [ExplicitKey]
        public int ID { get; set; }
        public string M_KANBAN01 { get; set; }
        public bool M_KANBAN02 { get; set; }
        public bool M_KANBAN03 { get; set; }
        public bool M_KANBAN04 { get; set; }
        public bool M_KANBAN05 { get; set; }
        public bool M_KANBAN06 { get; set; }
        public string Lang_ID { get; set; }
    }
}
