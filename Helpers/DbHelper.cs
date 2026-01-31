using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Login.Helpers
{
    public static class DbHelper
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static SqlDataReader ExecuteSp(string spName, SqlParameter[] sqlParameters)
        {
            SqlConnection conn = new SqlConnection(_connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (sqlParameters != null)
                {
                    cmd.Parameters.AddRange(sqlParameters);
                }

                 conn.Open();

                return  cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                throw;

            }

        }

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

    }
}