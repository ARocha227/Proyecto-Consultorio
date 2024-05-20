using System;
using System.Collections.Generic;

namespace ConsulCrud.Server.Model;

public partial class Departamento
{
    public int Iddepartamento { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
