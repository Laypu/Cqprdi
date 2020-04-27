
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel.Search
{
    public class SubscriberSearchModel:SearchModelBase
    {
        public SubscriberSearchModel()
        {
        }
        public string Account { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string DateTo { get; set; }
        public string DateFrom { get; set; }
        public string Status { get; set; }
    }
}
