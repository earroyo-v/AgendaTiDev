using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Agenda_Web.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }
        [Authorize]
        [HttpGet("/edit-user")]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
