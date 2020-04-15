using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel
{
    public class Paging<T> where T : class
    {

        public Paging()
        {
            total = 0;
            rows = new List<T>();
            cnt = 0;
        }
        public int total { get; set; }
        public List<T> rows { get; set; }

        public int cnt { get; set; }
    }
}
