using Dapper;
using dotnet_todolist.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyNhom : ControllerBase
    {
        private readonly string _connectionString;
        public QuanLyNhom(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
        }

        // GET: api/<QuanLyNhom>/LayDanhSachNhomTheoNguoiDung
        [Route("LayDanhSachNhomTheoNguoiDung")]
        [HttpGet]
        public async Task<IActionResult> LayDanhSachNhomTheoNguoiDung(string email)
        {
           try
            {
                using (var conn = new SqlConnection(_connectionString))
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@email", email);

                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var accountResult = await conn.QueryAsync<Account>("Get_Account_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);

                    if (accountResult.FirstOrDefault() != null)
                    {
                        var groupResult = await conn.QueryAsync<Group>("Get_Group_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);
                        if (groupResult.FirstOrDefault() != null)
                        {
                            return StatusCode(StatusCodes.Status200OK, groupResult);
                        } else
                        {
                            return StatusCode(StatusCodes.Status204NoContent, "Không có dữ liệu");
                        }
                        
                    } else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Người dùng không tồn tại");
                    }

                   

                }
            }
             catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
            }
        }

        [Route("ThemNhomMoi")]
        [HttpPost]

        public IActionResult ThemNhomMoi(Group group )
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
        }

        [Route("CapNhatThongTinNhom")]
        [HttpPut]
        public IActionResult CapNhatThongTinNhom(string email, Group group)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
        }

        [Route("XoaNhom")]
        [HttpDelete]

        public IActionResult XoaNhom(string email, string tenNhom)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
        }


    }
}
