//using Agenda_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Model
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Email { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Foto { get; set; }
        public string? UrlPerfil { get; set; }
        public int IdRol { get; set; }
        public string Rol { get; set; }

        /*public static implicit operator UsuarioDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                FechaNacimiento = usuario.FechaNacimiento,
                Email = usuario.Email,
                NickName = usuario.NickName,
                Password = usuario.Password,
                Foto = usuario.Foto,
                UrlPerfil = usuario.UrlPerfil,
                IdRol = usuario.IdRol,
                Rol = usuario.IdRolNavigation?.Nombre
            };
        }*/
    }
}
