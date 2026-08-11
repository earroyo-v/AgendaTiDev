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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpGet("/profile-user")]
        public async Task<IActionResult> Profile()
        {
            try
            {
                int id = Convert.ToInt32(User.FindFirst("IdUsuario")?.Value);
                var user = await _usuario.GetUser(id);
                if (user.Error || user.Data == null)
                {
                    TempData["error"] = "Error inesperado";
                    return Redirect("/Home");
                }
                UserProfileViewModel profile = user.Data.ToDTO();
                return View(profile);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error inesperado";
                return Redirect("/Home");
            }
        }

        [Authorize]
        [HttpGet("/edit-user")]
        public async Task<IActionResult> Edit()
        {
            try
            {
                int id = Convert.ToInt32(User.FindFirst("IdUsuario")?.Value);
                var user = await _usuario.GetUser(id);
                if (user.Error || user.Data == null)
                {
                    TempData["error"] = "Error inesperado";
                    return Redirect("/Home");
                }
                UserProfileViewModel profile = user.Data.ToDTO();
                return View(profile);
            }
            catch (Exception)
            {
                TempData["error"] = "Error inesperado";
                return Redirect("/Home");
            }
        }
        [Authorize]
        [HttpPost("/edit-user")]
        public async Task<IActionResult> Edit(UserProfileViewModel profile)
        {
            try
            {
                if (profile.ArchivoImagen != null && profile.ArchivoImagen.Length > 0)
                {
                    var tiposPermitidos = new[] { "image/png", "image/jpeg" };

                    if (!tiposPermitidos.Contains(profile.ArchivoImagen.ContentType))
                    {
                        TempData["error"] = "Solo se permiten imágenes PNG o JPG";
                        return Redirect("/login");
                    }
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "img");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    profile.Foto = Guid.NewGuid().ToString() + Path.GetExtension(profile.ArchivoImagen.FileName);
                    var path = Path.Combine(uploadsFolder, profile.Foto);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await profile.ArchivoImagen.CopyToAsync(stream);
                    }
                }
                var user = profile.ToDTO();
                user.IdUsuario = Convert.ToInt32(User.FindFirst("IdUsuario")?.Value);
                var response = await _usuario.UpdateUser(user);
                if (response.Error)
                {
                    TempData["error"] = "Error inesperado";
                    return Redirect("/Home");
                }
                //TempData["error"] = "Error inesperado";
                return Redirect("/Home");
            }
            catch (Exception)
            {
                TempData["error"] = "Error inesperado";
                return Redirect("/edit-user");
            }
        }
        [Authorize]
        [HttpGet("/change-password")]
        public IActionResult ChangePssw()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return Redirect("/Home");
            }
        }
        [Authorize]
        [HttpPost("/change-password")]
        public async Task<IActionResult> ChangePssw(string NewPssw, string CurrentPssw)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(email))
                {
                    TempData["error"] = "Error inesperado";
                    return Redirect("/change-password");
                }
                var response = await _usuario.ChangePassword(email, CurrentPssw, NewPssw);
                if (response.Error)
                {
                    TempData["error"] = response.Message;
                    return Redirect("/change-password");
                }
                //TempData["error"] = "Error inesperado";
                return Redirect("/Home");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return Redirect("/change-password");
            }
        }
        [Authorize]
        [HttpGet("/delete-user")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                int id = Convert.ToInt32(User.FindFirst("IdUsuario")?.Value);
                var response = await _usuario.DeleteUser(id);
                if (response.Error)
                {
                    TempData["error"] = response.Message;
                    return Redirect("/Home");
                }
                return Redirect("/LogOut");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return Redirect("/Home");
            }
        }
        public async Task<JsonResult> EmailUnico(string email)
        {
            bool emailUnique = await _usuario.ValidateEmail(email);

            return Json(!emailUnique);
        }
    }
}
