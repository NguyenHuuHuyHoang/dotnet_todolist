using Dapper;
using dotnet_todolist.Models;
using System.Data.SqlClient;

namespace dotnet_todolist.DAO
{
    public class TodoDAO
    {
        public static async Task<int> createByGroupdId(string _connectionString, ThongTinThemMoiTodo data)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@name", data.Name);
                parameters.Add("@endDate", data.EndDate);
                parameters.Add("@statusId", data.StatusId);
                parameters.Add("@groupId", data.GroupId);
                parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                await conn.ExecuteAsync("Create_Todo_ByGroupId", parameters, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();

                return parameters.Get<int>("@id");
            }
        }

        public static async Task<IEnumerable<dynamic>> getTodoByGroupId(string _connectionString, int groupId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@groupId", groupId);


                var result = await conn.QueryAsync("Get_Todo_ByGroupId", parameters, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();

                return result;
            }
        }
    }
}
