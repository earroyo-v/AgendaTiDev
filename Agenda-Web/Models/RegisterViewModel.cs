using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Agenda_Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [Display(Name = "Nombre's")]
        public string Nombre { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [Display(Name = "Apellido Materno")]
        public string? ApellidoMaterno { get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public DateOnly FechaNacimiento { get; set; }
        [Required]
        [EmailAddress]
        //[Remote()]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string NickName { get; set; } = null!;
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        [RegularExpression(@"^(?=\S+$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "La contraseña debe tener mínimo 8 caracteres, una mayúscula, una minúscula, un número, un carácter especial y no contener espacios")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = null!;
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        [RegularExpression(@"^(?=\S+$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "La contraseña debe tener mínimo 8 caracteres, una mayúscula, una minúscula, un número, un carácter especial y no contener espacios")]
        [Display(Name = "Confirma tu Contraseña")]
        public string PasswordCheck { get; set; } = null!;
        public string? Foto { get; set; }
        public string? UrlPerfil { get; set; }
    }
}
