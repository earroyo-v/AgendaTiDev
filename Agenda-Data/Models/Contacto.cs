using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Agenda_Data.Models;

[Table("Contacto")]
public partial class Contacto
{
    [Key]
    public int IdContacto { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ApellidoPaterno { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? ApellidoMaterno { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    [Unicode(false)]
    public string? Foto { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Telefono { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public int IdUsuario { get; set; }

    [InverseProperty("IdContactoNavigation")]
    public virtual ICollection<ContactoRedSocial> ContactoRedSocials { get; set; } = new List<ContactoRedSocial>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Contactos")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
