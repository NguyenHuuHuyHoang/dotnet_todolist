using Dapper;
using dotnet_todolist.DAO;
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
        public async Task<IActionResult> LayDanhSachNhomTheoNguoiDung(int accountId)
        {
           try
            {
                var result = await GroupDAO.getAllByAccountId(_connectionString, accountId);
                return StatusCode(StatusCodes.Status200OK, result);
                
            }
             catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
            }
        }

        [Route("ThemNhomMoi")]
        [HttpPost]

        public async Task<IActionResult> ThemNhomMoi(ThongTinThemMoiGroup data)
        {
            try
            {
                int id = 0;

                id = await GroupDAO.createByAccountId(_connectionString, data);

                return StatusCode(StatusCodes.Status201Created, new {id = id});
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi kết nối server");
            }
            
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
