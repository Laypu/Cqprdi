using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel
{
    public class LoginViewModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Captcha { get; set; }

        public string LoginType { get; set; }

        public string ReturnUrl { get; set; }
    }
}
