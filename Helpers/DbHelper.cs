using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;


namespace Login.Helpers
{
    public static class DbHelper
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static async Task<SqlDataReader> ExecuteSp(string spName, SqlParameter[] sqlParameters)
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

                await conn.OpenAsync().ConfigureAwait(false);

                return await  cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection).ConfigureAwait(false);
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