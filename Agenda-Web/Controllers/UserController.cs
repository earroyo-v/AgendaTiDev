using Microsoft.AspNetCore.Mvc;

namespace Agenda_Web.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("/register")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet("/edit-user")]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
