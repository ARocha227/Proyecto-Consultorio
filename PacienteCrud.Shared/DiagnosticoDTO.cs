using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsulCrud.Shared
{
    public class DiagnosticoDTO
    {
        public int IdDiagnostico { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int IdNuevo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string MotivoConsulta { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string AntecedentesPatologicos { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string EstiloVida { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Resultado { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Observaciones { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int Evaluador { get; set; }

        public DateTime FechaRegistro { get; set; }

        public PacienteDTO? Paciente { get; set; }
        public ProfesionalDTO? Profesional { get; set; }
    }
}
