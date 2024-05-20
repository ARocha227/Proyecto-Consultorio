using System;
using System.Collections.Generic;

namespace ConsulCrud.Server.Model;

public partial class Paciente
{
    public int IdNuevo { get; set; }

    public int IdPaciente { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public int Iddepartamento { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public int Telefono { get; set; }

    public string Direccion { get; set; } = null!;

    public string EstadoCivil { get; set; } = null!;

    public string Nacionalidad { get; set; } = null!;

    public string FechaNacimiento { get; set; } = null!;

    public DateTime HoraRegistro { get; set; }

    public virtual ICollection<DiagnosticoPaciente> DiagnosticoPacientes { get; set; } = new List<DiagnosticoPaciente>();

    public virtual Departamento IddepartamentoNavigation { get; set; } = null!;
}
