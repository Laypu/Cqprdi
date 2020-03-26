using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Oaww.Business;
using System.Security.Principal;
using System.Security.Claims;

namespace Oaww.Filter
{
    public class AuthFilter : ActionFilterAttribute
    {
        public string _FuncionID { get; set; }
        public string _paramter { get; set; }
        CommonService _commonService = new CommonService();
        private string parameterValue = string.Empty;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ClaimsIdentity id = null;
            string menuindex = string.Empty;
            string account = string.Empty;
            if (filterContext.HttpContext.User != null)
            {
                id = filterContext.HttpContext.User.Identity as ClaimsIdentity;
            }



            var name = id.FindFirst(ClaimTypes.NameIdentifier);
            account = name == null ? "" : name.Value;

            var roles = id.FindFirst(ClaimTypes.Role);

            string[] Roles = roles == null ? new string[] { } : new string[] { roles.Value };

            if (filterContext.Controller.ValueProvider.GetValue("menuindex") != null)
            {
                menuindex = filterContext.Controller.ValueProvider.GetValue("menuindex").AttemptedValue;
            }

            if (!string.IsNullOrEmpty(_paramter) && filterContext.ActionParameters.ContainsKey(_paramter))
            {
                parameterValue = filterContext.ActionParameters[_paramter].ToString();
            }

            //由左方menu來的
            if (!Roles.Contains("administrators") && string.IsNullOrEmpty(menuindex) == false)
            {
                if (!_commonService.CheckMenuAuth(Roles, "1", menuindex))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "Index",
                        Auth = "N",
                        area = "webadmin"
                    }));
                }

            } //由上方menu來的
            else if (!Roles.Contains("administrators") && !_commonService.CheckMenuAuthPara(Roles, "0", _FuncionID, string.IsNullOrEmpty(parameterValue) ? "" : _paramter + "=" + parameterValue, account))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index",
                    Auth = "N",
                    area = "webadmin"
                }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
