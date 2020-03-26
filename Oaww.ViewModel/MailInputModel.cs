using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class MailInputModel
    {
        public MailInputModel()
        {
            InputKey = new List<string>();
            InputValue = new List<string>();
        }

        public int ID { get; set; }
        public int MainID { get; set; }
        public DateTime CreateDatetime { get; set; }
        public IList<string> InputKey { get; set; }
        public IList<string> InputValue { get; set; }
        public string Progress { get; set; }
        public string ReplyStatus { get; set; }
        public string ReponseDept { get; set; }

        public string ReponseUser { get; set; }

        public DateTime? ResponseTime { get; set; }
        public FormInputNote[] ProcessNote { get; set; }
        public FormInputNote[] ReplyNote { get; set; }
    }

}
