using Agenda_BSS.Interfaces;
using Agenda_Web.Models;
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
        public UserController(IUsuario usuario)
        {
            _usuario = usuario;
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
                var validation = await _usuario.CreateUser(new Agenda_Model.UsuarioDTO
                {
                    Nombre = user.Nombre,
                    ApellidoPaterno = user.ApellidoPaterno,
                    ApellidoMaterno = user.ApellidoMaterno,
                    FechaNacimiento = user.FechaNacimiento,
                    Email = user.Email,
                    NickName = user.NickName,
                    Password = user.Password,
                    IdRol = 2,
                    //Foto = "default.jpg",
                    //UrlPerfil = "/images/default.jpg"
                });
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
    }
}
