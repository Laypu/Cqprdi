using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Oaww.DAL;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.IO;



namespace Oaww.BaseBusiness
{
    public abstract class BaseBusiness
    {
        protected static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected string _strConnectionString = null;

        public ConnectionStringSettings ConnectionSetting
        {
            set;
            get;
        }

        public string connectionString
        {
            get
            {
                return this._strConnectionString;
            }
        }

        protected List<SqlParameter> Parameter = new List<SqlParameter>();

        

        public BaseBusiness()
        {
            _strConnectionString = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        }

        public BaseBusiness(ConnectionStringSettings conn)
        {
            this.ConnectionSetting = conn;
        }

        protected SqlConnection OpenConnection()
        {
            // 若有連接設定參數，則優先以連接設定參數開啟連接
            if (this.ConnectionSetting != null)
            {
                return NewDBConnection(this.ConnectionSetting);
            }

            return NewDBConnection(_strConnectionString);
        }

        protected SqlConnection OpenConnection(int sec)
        {
            var sscsb = new SqlConnectionStringBuilder(_strConnectionString);
            sscsb.ConnectTimeout = sec;

            return NewDBConnection(sscsb.ConnectionString);
        }

        protected void CloseConnection(SqlConnection AConnection)
        {
            if (AConnection != null && AConnection.State == ConnectionState.Open)
            {
                AConnection.Close();
            }
        }

        protected SqlTransaction GetTransaction(SqlConnection AConnection)
        {
            return AConnection.BeginTransaction();
        }

        protected SqlTransaction GetTransaction(SqlConnection AConnection, IsolationLevel level)
        {
            return AConnection.BeginTransaction(IsolationLevel.Snapshot);
        }

        protected int ExeNonQuery(string sSql)
        {
            return this.ExeNonQuery(sSql, false);
        }

        protected int ExeNonQuery(string Sql, bool bPrepare)
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                // 實例化命令
                SqlCommand NewCommand = CommandUnit.NewCommand(Sql, AConnection);

                // 加入參數
                NewCommand.AddParameters(this.Parameter);

                // 返回結果
                return NewCommand.ExeNonQuery();
            }
        }

        protected int SMOExeNonQuery(string Sql)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                ServerConnection svrConnection = new ServerConnection(conn);
                Server server = new Server(svrConnection);
                return server.ConnectionContext.ExecuteNonQuery(Sql);
            }
        }

        protected int ExeNonQuery(string Sql, SqlTransaction ATans)
        {
            // 實例化命令
            SqlCommand NewCommand = CommandUnit.NewCommand(Sql, ATans.Connection, ATans);

            // 加入參數
            NewCommand.AddParameters(this.Parameter);

            // 返回結果
            return NewCommand.ExeNonQuery();
        }

        protected int ExecuteNonQuerySP(string spName)
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                // 實例化命令
                SqlCommand newCommand = CommandUnit.NewCommand(AConnection);

                // 存儲過程類型
                newCommand.CommandType = CommandType.StoredProcedure;

                // 存儲過程名稱
                newCommand.CommandText = spName;

                // 加入參數
                newCommand.AddParameters(this.Parameter);

                // 返回結果
                return newCommand.ExeNonQuery(); ;
            }
        }

        protected int ExecuteNonQuerySP(string spName, SqlTransaction ATans)
        {
            // 實例化命令
            SqlCommand newCommand = CommandUnit.NewCommand(ATans.Connection, ATans);

            // 存儲過程類型
            newCommand.CommandType = CommandType.StoredProcedure;

            // 存儲過程名稱
            newCommand.CommandText = spName;

            // 加入參數
            newCommand.AddParameters(this.Parameter);

            // 返回結果
            return newCommand.ExeNonQuery(); ;
        }

        protected object ExecuteScalar(string Sql)
        {
            return this.ExecuteScalar(Sql, false);
        }

        protected object ExecuteScalar(string Sql, string defaultValue)
        {
            var obj = this.ExecuteScalar(Sql, false);

            return obj == null ? defaultValue : obj;
        }

        protected object ExecuteScalar(string Sql, string defaultValue, SqlTransaction tran)
        {
            var obj = this.ExecuteScalar(Sql, tran);

            return obj == null ? defaultValue : obj;
        }

        protected object ExecuteScalar(string Sql, SqlTransaction tran)
        {
            return this.ExecuteScalar(Sql, tran, false);
        }

        protected object ExecuteScalar(string Sql, bool bPrepare)
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                // 實例化命令
                SqlCommand NewCommand = CommandUnit.NewCommand(Sql, AConnection);

                // 加入參數
                NewCommand.AddParameters(this.Parameter);

                // 返回結果物件
                var obj =  NewCommand.ExeScalar();

                NewCommand.Parameters.Clear();

                return obj;
            }
        }

        protected object ExecuteScalar(string Sql, SqlTransaction tran, bool bPrepare)
        {

            // 實例化命令
            SqlCommand NewCommand = CommandUnit.NewCommand(Sql, tran.Connection, tran);

            // 加入參數
            NewCommand.AddParameters(this.Parameter);

            // 返回結果物件
            return NewCommand.ExeScalar();

        }

        protected SqlDataReader ExecuteReader(string Sql)
        {
            return this.ExecuteReader(Sql, false);
        }

        protected SqlDataReader ExecuteReader(string Sql, bool bPrepare)
        {
            // 實例化并開啟連接
            SqlConnection AConnection = this.OpenConnection();

            // 實例化命令
            SqlCommand
            NewCommand = CommandUnit.NewCommand(Sql, AConnection);

            // 加入參數
            NewCommand.AddParameters(this.Parameter);

            // 返回Reader
            return NewCommand.ExeReader(CommandBehavior.CloseConnection);
        }

        protected DataTable Search(string Sql)
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                // 實例化命令
                SqlCommand NewCommand = CommandUnit.NewCommand(Sql, AConnection);

                NewCommand.Parameters.Clear();
                // 加入參數
                NewCommand.AddParameters(this.Parameter);

                // 返回結果集
                DataTable dt = NewCommand.Search();

                NewCommand.Parameters.Clear();

                return dt;
            }
        }

        protected DataTable Search(string Sql, SqlTransaction tran)
        {
            // 實例化命令
            SqlCommand NewCommand = CommandUnit.NewCommand(Sql, tran.Connection, tran);

            // 加入參數
            NewCommand.AddParameters(this.Parameter);

            // 返回結果集
            DataTable dt = NewCommand.Search();

            NewCommand.Parameters.Clear();

            return dt;
        }

        protected DataTable SearchFastPage(string Sql, int pageIndex, int pageSize, string order)
        {
            return Search(SetFastPageSql(Sql, pageIndex, pageSize, order));
        }

        protected DataTable SearchFastPage(string Sql, int pageIndex, int pageSize, string order, string sqlOrder)
        {
            return Search(SetFastPageSql(Sql, pageIndex, pageSize, order, sqlOrder));
        }

        protected DataTable SearchFastPage(string Sql, int pageIndex, int pageSize, string order, SqlTransaction tran)
        {
            return Search(SetFastPageSql(Sql, pageIndex, pageSize, order), tran);
        }

        protected DataTable SearchPage(string Sql, int PageIndex, int PageSize, string Order)
        {
            return Search(SetPageSql(Sql, PageIndex, PageSize, Order));
        }

        protected int SearchCount(string Sql)
        {
            return (int)this.ExecuteScalar(SetCountSql(Sql));
        }

        protected int SearchCount(string Sql, SqlTransaction tran)
        {
            return (int)this.ExecuteScalar(SetCountSql(Sql), tran, false);
        }

        protected DataTable ExecuteSP(string spName)
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                // 實例化命令
                SqlCommand NewCommand = CommandUnit.NewCommand(AConnection);

                // 存儲過程命令類型
                NewCommand.CommandType = CommandType.StoredProcedure;

                // 命令文本
                NewCommand.CommandText = spName;

                // 加入參數
                NewCommand.AddParameters(this.Parameter);

                // 返回查詢的結果集
                return NewCommand.Search();
            }
        }

        protected DataTable ExecuteSP(string spName, SqlTransaction tran)
        {

            // 實例化命令
            SqlCommand
            NewCommand = CommandUnit.NewCommand(tran.Connection, tran);

            // 存儲過程命令類型
            NewCommand.CommandType = CommandType.StoredProcedure;

            // 命令文本
            NewCommand.CommandText = spName;

            // 加入參數
            NewCommand.AddParameters(this.Parameter);

            // 返回查詢的結果集
            return NewCommand.Search();
        }

        protected List<T> ExecuteSP<T>(string Sql) where T : class
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                var spParams = new DynamicParameters();

                foreach (SqlParameter p in this.Parameter)
                {

                    spParams.Add(p.ParameterName, p.Value, p.DbType);
                }

                IEnumerable<T> Result = AConnection.Query<T>(Sql, spParams, commandType: CommandType.StoredProcedure);

                return Result.ToList();
            }
        }

        private string SetPageSql(string strSql, int start, int pageSize, string strOrder)
        {
            string sql = strSql
                         + strOrder
                         + $" Offset { start} Rows"
                         + $" Fetch Next {pageSize} Rows Only";

            return sql;
        }

        private string SetFastPageSql(string strSql, int page, int pageSize, string strOrder)
        {
            StringBuilder builder = new StringBuilder();

            int insertPoint = strSql.ToUpper().IndexOf("SELECT") + 6;

            strSql = strSql.Substring(0, insertPoint) + $" top 999999999  ROW_NUMBER() OVER (  {strOrder} ) AS ROWNUM, " + strSql.Substring(insertPoint);

            builder.Append("  SELECT * FROM (  ");
            builder.Append(strSql);
            builder.Append(" ) AS TB_PAGE");
            builder.Append(" WHERE ");
            builder.Append(" (");
            builder.Append(" TB_PAGE.ROWNUM ");
            builder.Append(" BETWEEN ( " + page + @"-1) * " + pageSize + @" + 1 ");
            builder.Append(" AND " + page + @" * " + pageSize + @"");
            builder.Append(" ) order by ROWNUM");

            return builder.ToString();
        }

        private string SetFastPageSql(string strSql, int page, int pageSize, string strOrder, string sqlOrder)
        {
            StringBuilder builder = new StringBuilder();

            int insertPoint = strSql.ToUpper().IndexOf("SELECT") + 6;

            strSql = strSql.Substring(0, insertPoint) + $"  ROW_NUMBER() OVER (  {strOrder} ) AS ROWNUM, " + strSql.Substring(insertPoint);

            builder.Append("  SELECT * FROM (  ");
            builder.Append(strSql);
            builder.Append(" ) AS TB_PAGE");
            builder.Append(" WHERE ");
            builder.Append(" (");
            builder.Append(" TB_PAGE.ROWNUM ");
            builder.Append(" BETWEEN ( " + page + @"-1) * " + pageSize + @" + 1 ");
            builder.Append(" AND " + page + @" * " + pageSize + @"");
            builder.Append(" ) " + sqlOrder);

            return builder.ToString();
        }

        public string SetCountSql(string strSql)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(" SELECT COUNT(0) ");
            builder.Append(" FROM (");
            builder.Append(strSql);
            builder.Append(" ) AS TEMP ");

            return builder.ToString();
        }

        protected List<T> SearchListPage<T>(string Sql, int start, int pageSize, string strOrder)
        {
            return SearchList<T>(SetPageSql(Sql, start, pageSize, strOrder));
        }

        protected List<T> SearchList<T>(string Sql)
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                var spParams = new DynamicParameters();

                foreach (SqlParameter p in this.Parameter)
                {

                    spParams.Add(p.ParameterName, p.Value, p.DbType);
                }

                IEnumerable<T> Result = AConnection.Query<T>(Sql, spParams);

                return Result.ToList();

            }
        }

        protected List<T> SearchList<T>(string Sql, SqlTransaction tran)
        {
            SqlConnection AConnection = tran.Connection;

            var spParams = new DynamicParameters();

            foreach (SqlParameter p in this.Parameter)
            {

                spParams.Add(p.ParameterName, p.Value, p.DbType);
            }


            IEnumerable<T> Result = AConnection.Query<T>(Sql, spParams, tran);

            return Result.ToList();

        }

        protected List<T> SearchList<T>(string Sql, int Start, int PageSize, string Order)
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                var spParams = new DynamicParameters();

                foreach (SqlParameter p in this.Parameter)
                {

                    spParams.Add(p.ParameterName, p.Value, p.DbType);
                }

                IEnumerable<T> Result = AConnection.Query<T>(SetPageSql(Sql, Start, PageSize, Order), spParams);

                return Result.ToList();

            }
        }

        protected List<T> SearchList<T>(string Sql, int Start, int PageSize, string Order,SqlTransaction tran)
        {
            using (SqlConnection AConnection = this.OpenConnection())
            {
                var spParams = new DynamicParameters();

                foreach (SqlParameter p in this.Parameter)
                {

                    spParams.Add(p.ParameterName, p.Value, p.DbType);
                }

                IEnumerable<T> Result = AConnection.Query<T>(SetPageSql(Sql, Start, PageSize, Order), spParams, tran);

                return Result.ToList();

            }
        }

        protected dynamic InsertObject<T>(T obj) where T : class
        {
            dynamic result;
            using (SqlConnection conn = this.OpenConnection())
            {
                result = conn.Insert<T>(obj);
            }

            return result;
        }

        protected long InsertObject<T>(T obj, SqlTransaction tran) where T : class
        {
            var conn = tran.Connection;

            long id = 0;
            
            id = conn.Insert<T>(obj, tran);
            

            return id;
        }

        protected bool DeleteObject<T>(T obj) where T : class
        {
            using (SqlConnection conn = this.OpenConnection())
            {
                return conn.Delete<T>(obj);
            }
        }

        protected bool DeleteObject<T>(T obj, SqlTransaction tran) where T : class
        {
            var conn = tran.Connection;
            return conn.Delete<T>(obj, tran);
          
        }

        protected bool UpdateObject<T>(T obj) where T : class
        {
            using (SqlConnection conn = this.OpenConnection())
            {
              return  conn.Update<T>(obj);
            }
        }

        protected bool UpdateObject<T>(T obj, SqlTransaction tran) where T : class
        {
            var conn = tran.Connection;
              return  conn.Update<T>(obj, tran);
           
        }

        protected T GetObject<T>(string sql)
        {
            return this.SearchList<T>(sql).FirstOrDefault();
        }

        protected T GetObject<T>(string sql, SqlTransaction tran)
        {
            return this.SearchList<T>(sql, tran).FirstOrDefault();
        }

        public bool ExtendInsert<T>(T entity) where T : class
        {
            try
            {
                InsertObject<T>(entity);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool ExtendInsert<T>(T entity, SqlTransaction tran) where T : class
        {
            try
            {
                InsertObject<T>(entity, tran);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool ExtendInsertList<T>(List<T> ListEntity) where T : class
        {
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlTransaction tran = GetTransaction(connection))
                {
                    try
                    {
                        ListEntity.ForEach(t =>
                        {
                            InsertObject<T>
(t, tran);
                        });

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }

        public void ExtendInsertList<T>(List<T> ListEntity, SqlTransaction tran) where T : class
        {

            try
            {
                ListEntity.ForEach(t =>
                {
                    InsertObject<T>(t, tran);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteGeneric<T>(string constrain)
        {
            string sql = @"delete " + typeof(T).Name + " where " + constrain;

            this.Parameter.Clear();

            try
            {
                ExeNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteGeneric<T>(string constrain, SqlTransaction tran)
        {
            string sql = @"delete " + typeof(T).Name + " where " + constrain;

            this.Parameter.Clear();

            try
            {
                ExeNonQuery(sql, tran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SqlConnection NewDBConnection(string connectionString)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();

            return sqlConn;
        }

        public static SqlConnection NewDBConnection(System.Configuration.ConnectionStringSettings connection)
        {
            if (connection == null)
            {
                throw new Exception("沒有連接配置信息！");
            }

            // 創建IDBConnection
            var con = new SqlConnection();

            // 連接初始化
            con.ConnectionString = connection.ConnectionString;

            // 打開連接
            con.Open();

            return con;
        }

    }
}
