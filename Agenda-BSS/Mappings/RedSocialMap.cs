using Agenda_Data.Models;
using Agenda_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Mappings
{
    public static class RedSocialMap
    {
        public static DetalleDTO ToDTO(this ContactoRedSocial redSocial)
        {
            return new DetalleDTO
            {
                IdContactoRedSocial = redSocial.IdContactoRedSocial,
                IdContacto = redSocial.IdContacto,
                IdRedSocial = redSocial.IdRedSocial,
                UrlPerfil = redSocial.UrlPerfil,
                NombreRedSocial = redSocial.IdRedSocialNavigation.Nombre
            };
        }
        public static DetalleDTO ToDTO(this RedSocial redSocial)
        {
            return new DetalleDTO
            {
                IdRedSocial = redSocial.IdRedSocial,
                NombreRedSocial = redSocial.Nombre
            };
        }
    }
}
