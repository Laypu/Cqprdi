using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.ViewModel
{
    public class AdminMemberModel
    {
        public int ID { get; set; }
        public string EncryptID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        
    }
}
