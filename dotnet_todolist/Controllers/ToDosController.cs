using Dapper;
using dotnet_todolist.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly string _connectionString;

        public ToDosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
        }


        // GET: api/<ToDosController>
        [HttpGet]
        public async Task<IEnumerable<ToDo>> Get()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var result = await conn.QueryAsync<ToDo>("Get_All_Todo",null,null,null, System.Data.CommandType.StoredProcedure);
                return result;

            }

           
            
        }

        // GET api/<ToDosController>/5
        [HttpGet("{id}")]
        public async Task<ToDo> Get(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = await conn.QueryAsync<ToDo>("Get_Todo_By_Id", parameters, null, null, System.Data.CommandType.StoredProcedure);
                return result.Single();

            }
        }

        // POST api/<ToDosController>
        [HttpPost]
        
        public async Task<int> Post([FromBody]  ToDo todo)
        {
            int newId = 0;

            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@content", todo.Content);
                parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var result = await conn.ExecuteAsync("Create_NewTodo", parameters, null, null, System.Data.CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }

            return newId;
        }

        // PUT api/<ToDosController>/5
        [HttpPut("{id}")]
        public async Task Put([FromRoute] int id, [FromBody] ToDo toDo) // Do Table Todo có Content not Null nên phải FE gửi phải có content lên nếu không sẽ lỗi
        {
            
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var parameters = new DynamicParameters();

                
                
                parameters.Add("@isCompleted", toDo.isCompleted);
                parameters.Add("@id", id);
                await conn.ExecuteAsync("Update_Todo_ById", parameters, null, null, System.Data.CommandType.StoredProcedure);
            }

           

        }

        // DELETE api/<ToDosController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                await conn.ExecuteAsync("Delete_By_Id", parameters, null, null, System.Data.CommandType.StoredProcedure);

            }
        }
    }
}
