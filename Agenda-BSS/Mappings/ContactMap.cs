using Agenda_Data.Models;
using Agenda_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Mappings
{
    public static class ContactMap
    { 
        public static ContactoDTO ToDTO(this Contacto contacto)
        {
            return new ContactoDTO
            {
                IdContacto = contacto.IdContacto,
                Nombre = contacto.Nombre,
                ApellidoPaterno = contacto.ApellidoPaterno,
                ApellidoMaterno = contacto.ApellidoMaterno,
                FechaNacimiento = contacto.FechaNacimiento,
                Foto = contacto.Foto,
                Telefono = contacto.Telefono,
                Email = contacto.Email,
                IdUsuario = contacto.IdUsuario,
                ContactoRedSocials = contacto.ContactoRedSocials.Select(crs => new DetalleDTO
                {
                    IdContactoRedSocial = crs.IdContactoRedSocial,
                    IdContacto = crs.IdContacto,
                    IdRedSocial = crs.IdRedSocial,
                    UrlPerfil = crs.UrlPerfil,
                    NombreRedSocial = crs.IdRedSocialNavigation.Nombre
                }).ToList()
            };
        }
    }
}
