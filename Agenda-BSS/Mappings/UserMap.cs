using Agenda_Data.Models;
using Agenda_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Mappings
{
    public static class UsuarioExtensions
    {
        public static UsuarioDTO ToDTO(this Usuario usuario)
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
        }
    }
}
