using Agenda_BSS.Interfaces;
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
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var validation = await _usuario.ValidateUser(email, password);
                if (validation.Error)
                {
                    return View(validation);
                }
                var user = validation.Data;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Nombre),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,"Admin")
                };

                var identity = new ClaimsIdentity(claims, "AgendaCookie");

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AgendaCookie", principal);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
