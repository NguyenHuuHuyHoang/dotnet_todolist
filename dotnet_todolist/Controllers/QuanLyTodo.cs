using dotnet_todolist.DAO;
using dotnet_todolist.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyTodo : ControllerBase
    {
        private readonly string _connectionString;
        public QuanLyTodo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
        }

        // GET: api/<QuanLyTodo>/LayDanhSachTodoTheoNhom
        [Route("LayDanhSachTodoTheoNhom")]
        [HttpGet]
        public async Task<IActionResult> LayDanhSachTodoTheoNhom(int groupId)
        {
            try
            {
                var result = await TodoDAO.getTodoByGroupId(_connectionString, groupId);
                return StatusCode(StatusCodes.Status200OK, result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
            }
        }

        // POST: api/<QuanLyTodo>/ThemTodoMoi
        [Route("ThemTodoMoi")]
        [HttpPost]

        public async Task<IActionResult> ThemTodoMoi(ThongTinThemMoiTodo data)
        {
            try
            {
                int newTodoId = await TodoDAO.createByGroupdId(_connectionString, data);

                int accountId = await GroupDAO.getAccountIdByGroupId(_connectionString, data.GroupId);

                await Account_TodoDAO.addAccountToTodo(_connectionString, accountId, newTodoId);

                return StatusCode(StatusCodes.Status201Created, new {id = newTodoId});
            }
             catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
            }
           
        }

        // POST: api/<QuanLyTodo>/ThemNguoiThucHienTodo
        [Route("ThemNguoiThucHienTodo")]
        [HttpPost]

        public IActionResult ThemNguoiThucHienTodo(ToDo todo)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
        }



        // PUT: api/<QuanLyTodo>/CapNhatThongTinTodo
        [Route("CapNhatThongTinTodo")]
        [HttpPut]
        public IActionResult CapNhatThongTinTodo(string email, ToDo todo)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
        }


        // DELETE: api/<QuanLyTodo>/XoaTodo
        [Route("XoaTodo")]
        [HttpDelete]

        public IActionResult XoaTodo(string email, string tenNhom)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
        }
    }
}
