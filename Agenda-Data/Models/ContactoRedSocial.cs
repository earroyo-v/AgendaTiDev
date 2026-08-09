using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Agenda_Data.Models;

[Table("ContactoRedSocial")]
public partial class ContactoRedSocial
{
    [Key]
    public int IdContactoRedSocial { get; set; }

    public int IdContacto { get; set; }

    public int IdRedSocial { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string UrlPerfil { get; set; } = null!;

    [ForeignKey("IdContacto")]
    [InverseProperty("ContactoRedSocials")]
    public virtual Contacto IdContactoNavigation { get; set; } = null!;

    [ForeignKey("IdRedSocial")]
    [InverseProperty("ContactoRedSocials")]
    public virtual RedSocial IdRedSocialNavigation { get; set; } = null!;
}
