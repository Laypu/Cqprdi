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
            //�u�@���q�����ɰ��檺�{���X�C
            //�`�N: �u���b Web.config �ɱN sessionstate �Ҧ��]�w�� InProc �ɡA
            //�~�|�޵o Session_End �ƥ�C�p�G�N�u�@���q�Ҧ��]�w�� StateServer
            //�� SQLServer�A�N���|�޵o�o�Өƥ�C
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
