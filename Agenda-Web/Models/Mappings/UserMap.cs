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
                UrlPerfil = usuario.UrlPerfil,
                Activo = true
            };
        }

        public static UsuarioDTO ToDTO(this UserProfileViewModel usuario)
        {
            return new UsuarioDTO
            {
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                FechaNacimiento = usuario.FechaNacimiento,
                Email = usuario.Email,
                NickName = usuario.NickName,
                IdRol = 2,
                Foto = usuario.Foto,
                UrlPerfil = usuario.UrlPerfil
            };
        }
        public static UserProfileViewModel ToDTO(this UsuarioDTO usuario)
        {
            return new UserProfileViewModel
            {
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                FechaNacimiento = usuario.FechaNacimiento,
                Email = usuario.Email,
                NickName = usuario.NickName,
                Foto = usuario.Foto,
                UrlPerfil = usuario.UrlPerfil
            };
        }
    }
}
