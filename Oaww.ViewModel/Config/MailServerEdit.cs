using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel.Config
{
    public class MailServerEdit
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public bool SSL { get; set; }
        public string UserID { get; set; }
        public string PXD { get; set; }
    }
}
