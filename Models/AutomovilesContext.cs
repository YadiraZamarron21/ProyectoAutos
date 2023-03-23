using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoAutos1.Models;

public partial class AutomovilesContext : DbContext
{
    public AutomovilesContext()
    {
    }

    public AutomovilesContext(DbContextOptions<AutomovilesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auto> Auto { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=Automoviles", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Auto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("auto");

            entity.Property(e => e.Imagen).HasColumnType("text");
            entity.Property(e => e.Marca).HasMaxLength(30);
            entity.Property(e => e.Modelo).HasMaxLength(30);
            entity.Property(e => e.Motor).HasMaxLength(20);
            entity.Property(e => e.Precio).HasPrecision(10);
            entity.Property(e => e.Transmision).HasMaxLength(15);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Idventa).HasName("PRIMARY");

            entity.ToTable("venta");

            entity.HasIndex(e => e.IdAuto, "fk_IdAuto");

            entity.Property(e => e.DireccionComprador).HasMaxLength(50);
            entity.Property(e => e.FechaCompra)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreComprador).HasMaxLength(50);
            entity.Property(e => e.Ocupacion).HasMaxLength(50);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TipoPago)
                .HasMaxLength(9)
                .IsFixedLength();

            entity.HasOne(d => d.IdAutoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdAuto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_IdAuto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
