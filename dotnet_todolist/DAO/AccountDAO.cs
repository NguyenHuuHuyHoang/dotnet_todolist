using Dapper;
using dotnet_todolist.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace dotnet_todolist.DAO
{
    public class AccountDAO
    {

        public static async Task<IEnumerable<Account>> getAll(string _connectionString)
        {
            
                using (var conn = new SqlConnection(_connectionString))
                {

                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var userResult = await conn.QueryAsync<Account>("Get_Accounts", null, null, null, System.Data.CommandType.StoredProcedure);

                    conn.Close();
                    return userResult;

                }
            
        }

        public static async Task<Account> getByEmail(string _connectionString, string email)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@email", email);

                var result = await conn.QueryAsync<Account>("Get_Account_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();

                if (result.Count() > 0)
                {
                    return result.Single();
                } else
                {
                    return null;
                }

            }
        }

        public static async Task<int> create(string _connectionString, ThongTinDangKyAccount account )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@email", account.Email);
                parameters.Add("@fullName", account.FullName);
                parameters.Add("@password", account.Password);
                parameters.Add("@phone", account.Phone);
                parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                await conn.ExecuteAsync("Create_Account", parameters, null, null, System.Data.CommandType.StoredProcedure);

                conn.Close();

                return parameters.Get<int>("@id");
            }
        }


        public static async Task<bool> delete(string _connectionString, string email)
        {
                using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@email", email);

                await conn.ExecuteAsync("Delete_Account_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);
                conn.Close();

                return true;
            }
        }

    }
}
