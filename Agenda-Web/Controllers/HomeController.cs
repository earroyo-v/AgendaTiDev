using System.Diagnostics;
using Agenda_BSS.Interfaces;
using Agenda_Model;
using Agenda_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Agenda_Model.General;

namespace Agenda_Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsuario _usuario;
        private readonly IContacto _contacto;
        private readonly IWebHostEnvironment _env;

        public HomeController(IUsuario usuario, IContacto contacto, IWebHostEnvironment env)
        {
            _usuario = usuario;
            _contacto = contacto;
            _env = env;
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
            int id = Convert.ToInt32(User.FindFirst("IdUsuario")?.Value);
            var user = await _contacto.GetContactos(id);
            return Json(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContactos(ContactoDTO contacto, IFormFile ArchivoImagen)
        {
            Result<bool> validation = new();
            if (!ModelState.IsValid)
            {
                validation.Message = "Información Invalida";
                validation.Error = true;
                return Json(validation);
            }
            if (ArchivoImagen != null && ArchivoImagen.Length > 0)
            {
                var tiposPermitidos = new[] { "image/png", "image/jpeg" };

                if (!tiposPermitidos.Contains(ArchivoImagen.ContentType))
                {
                    TempData["error"] = "Solo se permiten imágenes PNG o JPG";
                    return Redirect("/login");
                }
                var uploadsFolder = Path.Combine(_env.WebRootPath, "img");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                contacto.Foto = Guid.NewGuid().ToString() + Path.GetExtension(ArchivoImagen.FileName);
                var path = Path.Combine(uploadsFolder, contacto.Foto);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ArchivoImagen.CopyToAsync(stream);
                }
            }

            contacto.IdUsuario = Convert.ToInt32(User.FindFirst("IdUsuario")?.Value);

            validation = await _contacto.CreateContacto(contacto);

            return Json(validation);
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
