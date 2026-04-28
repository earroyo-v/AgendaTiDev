using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Model
{
    public class ContactoDTO
    {
        public int IdContacto { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string? Foto { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; } = null!;
        public int IdUsuario { get; set; }

        public int Edad
        {
            get
            {
                int edad = DateTime.Now.Year - FechaNacimiento.Year;
                if (FechaNacimiento > DateOnly.FromDateTime(DateTime.Now).AddYears(-edad))
                {
                    edad--;
                }
                return edad;
            }
            set
            {
                Edad = value;
            }
        }
        public bool BirthDay
        {
            get
            {
                bool cumple = false;
                if (DateTime.Now.Day == FechaNacimiento.Day && DateTime.Now.Month == FechaNacimiento.Month)
                {
                    cumple = true;
                }
                return cumple;
            }
            set
            {
                BirthDay = value;
            }
        }

        public List<DetalleDTO> ContactoRedSocials { get; set; } = new List<DetalleDTO>();
    }
}
