using Dapper;
using dotnet_todolist.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace dotnet_todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyNguoiDung : ControllerBase
    {

        private readonly string _connectionString;
        public QuanLyNguoiDung(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
        }


        //GET api/<QuanLyNguoiDung>/LayDanhSachNguoiDung
        [Route("LayDanhSachNguoiDung")]
        [HttpGet]
        public async Task<IActionResult> LayDanhSachNguoiDung()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {

                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var userResult = await conn.QueryAsync<Account>("Get_Accounts", null, null, null, System.Data.CommandType.StoredProcedure);

                    return StatusCode(StatusCodes.Status200OK, userResult);

                }
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi server");
            }

        }


        // POST api/<QuanLyNguoiDung>/DangKy
        [Route("DangKy")]
        [HttpPost]
        public async Task<IActionResult> DangKy([FromBody] Account account)
        {

            try
            {
                int newId = 0;

                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@email", account.Email);


                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var userResult = await conn.QueryAsync<Account>("Get_Account_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);

                    if (userResult.FirstOrDefault() != null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Email đã tồn tại");
                    }

                    parameters.Add("@fullName", account.FullName);
                    parameters.Add("@password", account.Password);
                    parameters.Add("@phone", account.Phone);
                    parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);


                    var result = await conn.ExecuteAsync("Create_Account", parameters, null, null, System.Data.CommandType.StoredProcedure);

                    newId = parameters.Get<int>("@id");

                }

                return StatusCode(StatusCodes.Status201Created, new {newId = newId});
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi server");
            }

        }


        // POST api/<QuanLyNguoiDung>/DangNhap
        [Route("DangNhap")]
        [HttpPost]
        public async Task<IActionResult> DangNhap([FromBody] ThongTinDangNhap account)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@email", account.Email);
                    parameters.Add("@password", account.Password);


                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var userResult = await conn.QueryAsync<Account>("Get_Account_ByEmailAndPassword", parameters, null, null, System.Data.CommandType.StoredProcedure);

                    if (userResult.FirstOrDefault() != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, userResult);

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized, "Email hoặc mật khẩu không đúng");
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi server");
            }
 
        }

        // PUT api/<QuanLyNguoiDung>/CapNhatThongTinNguoiDung
        [Route("CapNhatThongTinNguoiDung")]
        [HttpPut]
        public async Task<IActionResult> CapNhatThongTinNguoiDung([FromBody] Account account)
        {

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@email", account.Email);


                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var userResult = await conn.QueryAsync<Account>("Get_Account_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);

                    //Do dapper trả ra mảng rỗng nếu query không có dữ liệu nên cần phải gọi FirstOrDefault để kiểm tra coi có đối tượng nào không, var không có thuộc tính length nên không check được bằng length
                    if (userResult.FirstOrDefault() != null)
                    {
                        parameters.Add("@fullName", account.FullName);
                        parameters.Add("@password", account.Password);
                        parameters.Add("@phone", account.Phone);
                        var result = await conn.ExecuteAsync("Update_Account_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);


                        return StatusCode(202, "Cập nhật thành công");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Người dùng không tồn tại");
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi truy xuất dữ liệu từ server");
            }

        }


        //DELETE api/<QuanLyNguoiDung>/XoaNguoiDung
        [Route("XoaNguoiDung")]
        [HttpDelete]

        public async Task<IActionResult> XoaNguoiDung(string email)
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

                    var result = await conn.QueryAsync<Account>("Get_Account_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);

                    if (result.FirstOrDefault() != null)
                    {
                        await conn.ExecuteAsync("Delete_Account_ByEmail", parameters, null, null, System.Data.CommandType.StoredProcedure);
                        return StatusCode(StatusCodes.Status202Accepted, "Xóa tài khoản thành công");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Tài khoản không tồn tại");
                    }
                }
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối tới server");
            }
        }
    }
}
