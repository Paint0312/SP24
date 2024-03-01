using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    // using Microsoft.Data.SqlClient

    namespace DemoWinformDAO.DAO
    {
        public class DAO
        {
            public static SqlConnection GetConnection()
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                String ConnectionStr = config.GetConnectionString("DB");
                return new SqlConnection(ConnectionStr);
            }

            public static DataTable GetDataBySQL(string sql, params SqlParameter[] parameters)
            {
                SqlCommand command = new SqlCommand(sql, GetConnection());
                if (parameters != null || parameters.Length == 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = command;
                DataTable dt = new DataTable();

                adapter.Fill(dt);
                return dt;
            }

            public static int ExcuteSql(String sql, params SqlParameter[] parameters)
            {
                SqlCommand command = new SqlCommand(sql, GetConnection());
                if (parameters != null || parameters.Length == 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.Connection.Open();
                int count = command.ExecuteNonQuery();
                command.Connection.Close();
                return count;
            }
        }
    }