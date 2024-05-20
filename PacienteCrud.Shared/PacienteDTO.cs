using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace ConsulCrud.Shared
{
    public class PacienteDTO
    {
        public int IdNuevo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int IdPaciente { get; set; }

        [Required (ErrorMessage ="El campo {0} es requerido.")]
        public string NombreCompleto { get; set; } = null!;

        [Required]
        [Range (1,int.MaxValue,ErrorMessage= "El campo {0} es requerido.")]
        public int Iddepartamento { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string CorreoElectronico { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string EstadoCivil { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Nacionalidad { get; set; } = null!;


        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string FechaNacimiento { get; set; } = null!;

        public DateTime HoraRegistro { get; set; }

        public DepartamentoDTO? Departamento { get; set; }
    }
}
