using Dapper;
using dotnet_todolist.Models;
using System.Data.SqlClient;

namespace dotnet_todolist.DAO
{
    public class StatusDAO
    {
        public static async Task<IEnumerable<Status>> getAll(string _connectionString)
        {

            using (var conn = new SqlConnection(_connectionString))
            {

                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var result = await conn.QueryAsync<Status>("Get_Status", null, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();
                return result;

            }

        }

    }
}
