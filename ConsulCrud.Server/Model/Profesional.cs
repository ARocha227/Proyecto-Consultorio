using System;
using System.Collections.Generic;

namespace ConsulCrud.Server.Model;

public partial class Profesional
{
    public int IdCodigoP { get; set; }

    public int IdIdentificador { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public int Telefono { get; set; }

    public string CdigoProfesional { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string FechaNacimiento { get; set; } = null!;

    public DateTime HoraRegistro { get; set; }

    public virtual ICollection<DiagnosticoPaciente> DiagnosticoPacientes { get; set; } = new List<DiagnosticoPaciente>();
}
