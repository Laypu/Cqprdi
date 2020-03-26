using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Oaww.DAL
{
    public static class CommandUnit
    {
        public static SqlCommand NewCommand(string sql,SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand(); 
 
            command.Connection = connection;

            command.CommandTimeout = 0;
        
            command.CommandText = sql;

            return command;
        }

        public static SqlCommand NewCommand(string sql, SqlConnection connection, SqlTransaction tansaction)
        {
            SqlCommand command = connection.CreateCommand(); //SwitchNewCommand(connection);

            // 設定Command連接
            command.Connection = connection;

            command.CommandTimeout = 0;

            // 設定Command事務
            command.Transaction = tansaction;

            // sql
            command.CommandText = sql;

            return command;
        }

        public static SqlCommand NewCommand(SqlConnection connection, SqlTransaction tansaction)
        {
            SqlCommand command = connection.CreateCommand(); //SwitchNewCommand(connection);

            // 設定Command連接
            command.Connection = connection;

            command.CommandTimeout = 0;

            // 設定Command事務
            command.Transaction = tansaction;

            return command;
        }

        public static SqlCommand NewCommand(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand(); //SwitchNewCommand(connection);

            // 設定Command連接
            command.Connection = connection;

            command.CommandTimeout = 0;

            return command;
        }

        private static SqlDataAdapter CreateDataAdapter(this SqlCommand cmd)
        {
            // SqlCommand
            if (cmd is SqlCommand)
            {
                return new SqlDataAdapter((SqlCommand)cmd);
            }

            // 返回Command
            throw new Exception("未知類型Command！");
        }

        public static SqlCommand AddParameters(this SqlCommand cmd, SqlParameterCollection parameters)
        {
            foreach(SqlParameter para in parameters)
            {
                cmd.Parameters.Add(para);
            }                        

            return cmd;
        }

        public static SqlCommand AddParameters(this SqlCommand cmd, List<SqlParameter> parameters)
        {
            foreach (SqlParameter para in parameters)
            {
                cmd.Parameters.Add(para);
            }

            return cmd;
        }

        public static int ExeNonQuery(this SqlCommand command)
        {
            return command.ExecuteNonQuery();
        }

        public static SqlDataReader ExeReader(this SqlCommand command, CommandBehavior behavior)
        {
            return command.ExecuteReader(behavior);
        }

        public static object ExeScalar(this SqlCommand command)
        {
            return command.ExecuteScalar();
        }

        public static DataTable Search(this SqlCommand command)
        {
            var da = command.CreateDataAdapter();

            DataSet dsResult = new DataSet();

            da.Fill(dsResult);

            return dsResult.Tables.Count == 0 ? new DataTable() : dsResult.Tables[0];
        }
    }
}
