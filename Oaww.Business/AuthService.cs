using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using System.Data.SqlClient;

namespace Oaww.Business
{
    public class AuthService : BaseBusiness.BaseBusiness
    {
        public Users ValidateUser(string LoginId, string Password)
        {
            string sql = @"select * from Users t where t.Account=@Account and t.PWD=@Password";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Account", LoginId));
            base.Parameter.Add(new SqlParameter("@Password", Password));

            return base.GetObject<Users>(sql);
        }
    }
}
