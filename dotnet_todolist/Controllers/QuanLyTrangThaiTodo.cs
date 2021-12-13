using dotnet_todolist.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyTrangThaiTodo : ControllerBase
    {

        private readonly string _connectionString;
        public QuanLyTrangThaiTodo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
        }

        // GET: api/<QuanLyTrangThaiTodo>/LayDanhSachTrangThai
        [Route("LayDanhSachTrangThai")]
        [HttpGet]
        public async Task<IActionResult> LayDanhSachTrangThai()
        {
            try
            {
                var result = await StatusDAO.getAll(_connectionString);
                return StatusCode(StatusCodes.Status200OK, result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
            }
        }
    }
}
