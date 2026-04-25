using System.Diagnostics;
using Agenda_BSS.Interfaces;
using Agenda_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsuario _usuario;
        private readonly IContacto _contacto;

        public HomeController(IUsuario usuario, IContacto contacto)
        {
            _usuario = usuario;
            _contacto = contacto;
        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            //var user = await _usuario.GetUser(1);
            //var user = await _contacto.GetContactos(1);
            return View();
        }

        public async Task<IActionResult> GetContactos()
        {
            var user = await _contacto.GetContactos(1);
            return Json(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
