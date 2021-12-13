using Dapper;
using dotnet_todolist.Models;
using System.Data.SqlClient;

namespace dotnet_todolist.DAO
{
    public class GroupDAO
    {
        public static async Task<IEnumerable<Group>> getAllByAccountId(string _connectionString, int accountId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId);

                var result = await conn.QueryAsync<Group>("Get_Groups_ByAccountId", parameters, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();

                if (result.Count() > 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }

            }
        }

        public static async Task<int> getAccountIdByGroupId(string _connectionString, int groupId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@groupId", groupId);

                var result = await conn.QueryFirstAsync<Group>("Get_Account_ByGroupId", parameters, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();

                return result.AccountId;

            }
        }

        public static async Task<int> createByAccountId(string _connectionString, ThongTinThemMoiGroup data)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@name", data.Name);
                parameters.Add("@desc", data.Description);
                parameters.Add("@accountId", data.AccountId);
                parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                await conn.ExecuteAsync("Create_Group_ByAccountId", parameters, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();

                return parameters.Get<int>("@id");
            }
        }
    }
}
       
