using Agenda_BSS.Interfaces;
using Agenda_Web.Models;
using Agenda_Web.Models.Mappings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Agenda_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsuario _usuario;
        private readonly IWebHostEnvironment _env;
        public UserController(IUsuario usuario, IWebHostEnvironment env)
        {
            _usuario = usuario;
            _env = env;
        }
        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Información Invalida";
                    return Redirect("/login");
                }
                if (user.ArchivoImagen != null && user.ArchivoImagen.Length > 0)
                {
                    var tiposPermitidos = new[] { "image/png", "image/jpeg" };

                    if (!tiposPermitidos.Contains(user.ArchivoImagen.ContentType))
                    {
                        TempData["error"] = "Solo se permiten imágenes PNG o JPG";
                        return Redirect("/login");
                    }
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "img");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    user.Foto = Guid.NewGuid().ToString() + Path.GetExtension(user.ArchivoImagen.FileName);
                    var path = Path.Combine(uploadsFolder, user.Foto);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await user.ArchivoImagen.CopyToAsync(stream);
                    }
                }

                var validation = await _usuario.CreateUser(user.ToDTO());

                if (validation.Error)
                {
                    TempData["error"] = validation.Message;
                    return Redirect("/login");
                }
                TempData["success"] = "Usuario Registrado!";
                return Redirect("/login");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error inesperado";
                return Redirect("/login");
            }
        }

        [Authorize]
        [HttpGet("/edit-user")]
        public IActionResult Edit()
        {
            return View();
        }
        public async Task<JsonResult> EmailUnico(string email)
        {
            bool emailUnique = await _usuario.ValidateEmail(email);

            return Json(!emailUnique);
        }
    }
}
