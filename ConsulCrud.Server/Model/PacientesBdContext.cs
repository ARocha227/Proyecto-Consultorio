using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsulCrud.Server.Model;

public partial class PacientesBdContext : DbContext
{
    public PacientesBdContext()
    {
    }

    public PacientesBdContext(DbContextOptions<PacientesBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DiagnosticoPaciente> DiagnosticoPacientes { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Profesional> Profesionals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Iddepartamento).HasName("PK__Departam__2E1CDB3481AC07EE");

            entity.ToTable("Departamento");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DiagnosticoPaciente>(entity =>
        {
            entity.HasKey(e => e.IdDiagnostico).HasName("PK__Diagnost__BD16DB69A16E6B8D");

            entity.ToTable("DiagnosticoPaciente");

            entity.Property(e => e.AntecedentesPatologicos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstiloVida)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.MotivoConsulta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Resultado)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EvaluadorNavigation).WithMany(p => p.DiagnosticoPacientes)
                .HasForeignKey(d => d.Evaluador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diagnosti__Evalu__6A30C649");

            entity.HasOne(d => d.IdNuevoNavigation).WithMany(p => p.DiagnosticoPacientes)
                .HasForeignKey(d => d.IdNuevo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diagnosti__IdNue__693CA210");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdNuevo).HasName("PK__Paciente__48487FA0C317F79F");

            entity.ToTable("Paciente");

            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoraRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IddepartamentoNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.Iddepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Paciente__Iddepa__619B8048");
        });

        modelBuilder.Entity<Profesional>(entity =>
        {
            entity.HasKey(e => e.IdCodigoP).HasName("PK__Profesio__1F0E02860EC8E4B8");

            entity.ToTable("Profesional");

            entity.Property(e => e.CdigoProfesional)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoraRegistro).HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
