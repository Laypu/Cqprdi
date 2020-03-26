using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;

namespace Oaww.ViewModel
{
    public class AdminFunctionModel
    {
        public AdminFunctionModel()
        {
            AdminFixFunctionInput = new List<AdminFunctionAuth>();
            AdminMenuFunctionInput = new List<AdminFunctionAuth>();
            AdminFunctionList = new Dictionary<int, List<AdminFunction>>();
            MenuList = new List<Menu>();

            GroupName = "";
        }
        public List<AdminFunctionAuth> AdminFixFunctionInput { get; set; }
        public List<AdminFunctionAuth> AdminMenuFunctionInput { get; set; }
        public Dictionary<int, List<AdminFunction>> AdminFunctionList { get; set; }
        public IList<Menu> MenuList { get; set; }
        public string GroupName { get; set; }
        public string GroupID { get; set; }
        public string Comment { get; set; }
    }
}
