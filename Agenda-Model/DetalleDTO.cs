using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Model
{
    public class DetalleDTO
    {
        public int IdContactoRedSocial { get; set; }
        public int IdContacto { get; set; }
        public int IdRedSocial { get; set; }
        public string UrlPerfil { get; set; } = null!;
        public string NombreRedSocial { get; set; } = null!;
        //public RedSocialDTO RedSocials { get; set; } = new();
    }
}
