using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Person ps) // Đổi từ IndexPost thành Index để khớp với view
        {
            if (ps == null)
            {
                ViewBag.infoPerson = "Dữ liệu không hợp lệ.";
                return View();
            }

            string strOutput = $"Xin chào {ps.PersonId} - {ps.FullName} - {ps.Address}";
            ViewBag.infoPerson = strOutput;

            return View("Index", ps); // Chỉ định trả về view Index.cshtml
        }
    }
}
