using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Oaww.Entity.SET
{
    public class SET_MENU
    {
        [ExplicitKey]
        public int M_Menu01 { get; set; }
        public string M_Menu02 { get; set; }
        public bool M_Menu03 { get; set; }
        public string M_Menu04 { get; set; }
        public string M_Menu05 { get; set; }
        public string M_Menu06 { get; set; }
        public bool M_Menu07 { get; set; }
        public string M_Menu08 { get; set; }
        public string M_Menu09 { get; set; }
        public string M_Menu10 { get; set; }
        public string M_Menu11 { get; set; }
    }
}
