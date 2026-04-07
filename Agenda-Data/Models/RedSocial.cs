using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Agenda_Data.Models;

[Table("RedSocial")]
public partial class RedSocial
{
    [Key]
    public int IdRedSocial { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdRedSocialNavigation")]
    public virtual ICollection<ContactoRedSocial> ContactoRedSocials { get; set; } = new List<ContactoRedSocial>();
}
