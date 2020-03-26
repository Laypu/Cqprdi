using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class FormViewModel
    {
        public string mid { get; set; }
        public string itemid { get; set; }

        public string btn { get; set; }
        public FormSetting FormSetting { get; set; }

        public List<FormSelItem> formSelItems { get; set; }
        public Dictionary<string, string> json { get; set; }
    }
}
