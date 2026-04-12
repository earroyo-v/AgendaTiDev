using Agenda_BSS.Interfaces;
using Agenda_Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Agenda_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuario _usuario;
        public AuthController(IUsuario usuario)
        {
            _usuario = usuario;
        }
        [HttpGet("/")]
        [HttpGet("/login")]
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Información Invalida";
                    return Redirect("/login");
                }
                var validation = await _usuario.ValidateUser(login.email, login.password);
                if (validation.Error)
                {
                    TempData["error"] = validation.Message;
                    return Redirect("/login");
                }
                var user = validation.Data;
                var claims = new List<Claim>
                {
                    new Claim("IdUsuario",user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name,user.Nombre),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,user.Rol)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                double expireTime = 3;

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(expireTime)
                });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error inesperado";
                return Redirect("/login");
            }
        }
        /*partial async Task<IActionResult> LogOut()
        {
            return "";
        }*/
    }
}
