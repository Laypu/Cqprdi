using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Dapper.Contrib.Extensions;

namespace Aofeng
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);    
            SqlMapperExtensions.TableNameMapper = (type) =>
            {
                //use exact name
                return type.Name;
            };
            Application["online"] = 0;
            Application["onlinedate"] = DateTime.Now.ToString("yyyyMMdd");
            Application["todayonline"] = 0;
        }

        void Session_End(object sender, EventArgs e)
        {
            //工作階段結束時執行的程式碼。
            //注意: 只有在 Web.config 檔將 sessionstate 模式設定為 InProc 時，
            //才會引發 Session_End 事件。如果將工作階段模式設定為 StateServer
            //或 SQLServer，就不會引發這個事件。
            Application.Lock();
            int online = (int)Application["online"];
            if (Session["LoginDate"] != null)
            {
                Application["online"] = online - 1;
            }
            Application.UnLock();
        }
    }
}
