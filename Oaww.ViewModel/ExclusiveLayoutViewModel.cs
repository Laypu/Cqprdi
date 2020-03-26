using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.Entity.SET;

namespace Oaww.ViewModel
{
    public class ExclusiveLayoutViewModel
    {
        public ExclusiveLayout ExclusiveLayout { get; set; }
        public List<SET_LAYOUT_GROUP> SET_LAYOUT_GROUPs { get; set; }

        public List<Users> Users { get; set; }
    }
}
