using Agenda_BSS.Interfaces;
using Agenda_Web.Models;
using Microsoft.AspNetCore.Authentication;
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
        [HttpGet("/login")]
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Redirect("/login");
                }
                var validation = await _usuario.ValidateUser(login.email, login.password);
                if (validation.Error)
                {
                    return Redirect("/login");
                }
                var user = validation.Data;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Nombre),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.NickName),
                    new Claim(ClaimTypes.Role,user.Rol)
                };

                var identity = new ClaimsIdentity(claims, "AgendaCookie");

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AgendaCookie", principal);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return Redirect("/login");
            }
        }
    }
}
