using dotnet_todolist.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyTodo : ControllerBase
    {
        // GET: api/<QuanLyTodo>/LayDanhSachTodoTheoNhom
        [Route("LayDanhSachTodoTheoNhom")]
        [HttpGet]
        public IEnumerable<string> LayDanhSachTodoTheoNhom()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/<QuanLyTodo>/ThemTodoMoi
        [Route("ThemTodoMoi")]
        [HttpPost]

        public IActionResult ThemTodoMoi(ToDo todo)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
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
