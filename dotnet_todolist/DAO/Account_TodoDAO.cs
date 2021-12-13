using Dapper;
using dotnet_todolist.Models;
using System.Data.SqlClient;

namespace dotnet_todolist.DAO
{
    public class Account_TodoDAO
    {
        public static async Task addAccountToTodo(string _connectionString,int accountId, int todoId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId);
                parameters.Add("@todoId", todoId);
                

                await conn.ExecuteAsync("Add_AccountTodo", parameters, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();
            }
        }
    }
}
