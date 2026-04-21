using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Model
{
    public class ContactoDTO
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string? Foto { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; } = null!;
        public int IdUsuario { get; set; }

        public List<DetalleDTO> ContactoRedSocials { get; set; } = new List<DetalleDTO>();
    }
}
