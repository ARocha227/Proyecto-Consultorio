using System;
using System.Collections.Generic;

namespace ConsulCrud.Server.Model;

public partial class DiagnosticoPaciente
{
    public int IdDiagnostico { get; set; }

    public int IdNuevo { get; set; }

    public string MotivoConsulta { get; set; } = null!;

    public string AntecedentesPatologicos { get; set; } = null!;

    public string EstiloVida { get; set; } = null!;

    public string Resultado { get; set; } = null!;

    public string Observaciones { get; set; } = null!;

    public int Evaluador { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Profesional EvaluadorNavigation { get; set; } = null!;

    public virtual Paciente IdNuevoNavigation { get; set; } = null!;
}
