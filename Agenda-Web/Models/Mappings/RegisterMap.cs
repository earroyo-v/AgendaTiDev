using Agenda_Data.Models;
using Agenda_Model;

namespace Agenda_Web.Models.Mappings
{
    public static class RegisterMap
    {
        public static UsuarioDTO ToDTO(this RegisterViewModel usuario)
        {
            return new UsuarioDTO
            {
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                FechaNacimiento = usuario.FechaNacimiento,
                Email = usuario.Email,
                NickName = usuario.NickName,
                Password = usuario.Password,
                IdRol = 2,
                Foto = usuario.Foto,
                UrlPerfil = usuario.UrlPerfil
            };
        }
    }
}
