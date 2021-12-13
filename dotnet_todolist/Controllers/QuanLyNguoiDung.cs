using Dapper;
using dotnet_todolist.DAO;
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


        //GET api/<QuanLyNguoiDung>/LayDanhSachNguoiDung - OK
        [Route("LayDanhSachNguoiDung")]
        [HttpGet]
        public async Task<IActionResult> LayDanhSachNguoiDung()
        {
            try
            {  
                var result = await AccountDAO.getAll(_connectionString);

                return StatusCode(StatusCodes.Status200OK, result);

            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi server");
            }
        }


        // POST api/<QuanLyNguoiDung>/DangKy - OK
        [Route("DangKy")]
        [HttpPost]
        public async Task<IActionResult> DangKy([FromBody] ThongTinDangKyAccount account)
        {

            try
            {
                if ((await AccountDAO.getByEmail(_connectionString, account.Email)) != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Tài khoản đã tồn tại");
                }
                else
                {
                    int newId = 0;
                    newId = await AccountDAO.create(_connectionString, account);

                    return StatusCode(StatusCodes.Status201Created, new { newId = newId });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi server");
            }

        }


        // POST api/<QuanLyNguoiDung>/DangNhap - CHƯA XỬ LÝ
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

        // PUT api/<QuanLyNguoiDung>/CapNhatThongTinNguoiDung CHƯA XỬ LÝ
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


        //DELETE api/<QuanLyNguoiDung>/XoaNguoiDung - OK
        [Route("XoaNguoiDung")]
        [HttpDelete]

        public async Task<IActionResult> XoaNguoiDung(string email)
        {
            try
            {
                if ((await AccountDAO.getByEmail(_connectionString, email)) != null)
                {
                    if (await AccountDAO.delete(_connectionString,email))
                    {
                        return StatusCode(StatusCodes.Status202Accepted, "Xóa tài khoản thành công");
                    } else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Xóa thất bại");
                    }
                    
                    
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Tài khoản không tồn tại");
                }
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối tới server");
            }
        }
    }
}
