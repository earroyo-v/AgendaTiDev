using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Agenda_Data.Models;

[Table("Usuario")]
[Index("Email", Name = "IX_Users_Email", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

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

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string NickName { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Unicode(false)]
    public string? Foto { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UrlPerfil { get; set; }

    public int IdRol { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();

    [ForeignKey("IdRol")]
    [InverseProperty("Usuarios")]
    public virtual Role IdRolNavigation { get; set; } = null!;
}
