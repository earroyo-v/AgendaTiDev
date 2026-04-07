using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Agenda_Data.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contacto> Contactos { get; set; }

    public virtual DbSet<ContactoRedSocial> ContactoRedSocials { get; set; }

    public virtual DbSet<RedSocial> RedSocials { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=GENERACION33;Trusted_Connection=True;TrustServerCertificate=True;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contacto>(entity =>
        {
            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Contactos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contacto_Usuario");
        });

        modelBuilder.Entity<ContactoRedSocial>(entity =>
        {
            entity.HasOne(d => d.IdContactoNavigation).WithMany(p => p.ContactoRedSocials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactoRedSocial_Contacto");

            entity.HasOne(d => d.IdRedSocialNavigation).WithMany(p => p.ContactoRedSocials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactoRedSocial_RedSocial");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
