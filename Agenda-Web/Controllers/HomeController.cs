using System.Diagnostics;
using Agenda_BSS.Interfaces;
using Agenda_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuario _usuario;

        public HomeController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _usuario.GetUser("erick");
            return View();
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
