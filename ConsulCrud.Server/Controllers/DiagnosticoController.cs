using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ConsulCrud.Server.Model;
using ConsulCrud.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ConsulCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticoController : ControllerBase
    {
        private readonly PacientesBdContext _dbContext;

        public DiagnosticoController(PacientesBdContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<DiagnosticoDTO>>();
            var listaDiagnosticoDTO = new List<DiagnosticoDTO>();
            
            try
            {
                foreach (var item in await _dbContext.DiagnosticoPacientes.Include(d => d.IdNuevoNavigation.DiagnosticoPacientes).Include (b => b.EvaluadorNavigation.DiagnosticoPacientes).ToListAsync())
                 
                {

                    listaDiagnosticoDTO.Add(new DiagnosticoDTO
                    {
                        IdDiagnostico = item.IdDiagnostico,
                        IdNuevo = item.IdNuevo,
                        MotivoConsulta = item.MotivoConsulta,
                        AntecedentesPatologicos = item.AntecedentesPatologicos,
                        EstiloVida = item.EstiloVida,
                        Resultado = item.Resultado,
                        Observaciones = item.Observaciones,
                        Evaluador = item.Evaluador,
                        FechaRegistro = item.FechaRegistro,

						Paciente = new PacienteDTO
						{
							IdNuevo = item.IdNuevoNavigation.IdNuevo,
							IdPaciente = item.IdNuevoNavigation.IdPaciente,
							NombreCompleto = item.IdNuevoNavigation.NombreCompleto,
							Iddepartamento = item.IdNuevoNavigation.Iddepartamento,
							CorreoElectronico = item.IdNuevoNavigation.CorreoElectronico,
							Telefono = item.IdNuevoNavigation.Telefono,
							Direccion = item.IdNuevoNavigation.Direccion,
							EstadoCivil = item.IdNuevoNavigation.EstadoCivil,
							Nacionalidad = item.IdNuevoNavigation.Nacionalidad,
							FechaNacimiento = item.IdNuevoNavigation.FechaNacimiento,
							HoraRegistro = item.IdNuevoNavigation.HoraRegistro,
						},


						 Profesional = new ProfesionalDTO
						 {
							 IdCodigoP = item.EvaluadorNavigation.IdCodigoP,
							 IdIdentificador = item.EvaluadorNavigation.IdIdentificador,
							 NombreCompleto = item.EvaluadorNavigation.NombreCompleto,
							 CorreoElectronico = item.EvaluadorNavigation.CorreoElectronico,
							 Telefono = item.EvaluadorNavigation.Telefono,
							 CdigoProfesional = item.EvaluadorNavigation.CdigoProfesional,
							 Direccion = item.EvaluadorNavigation.Direccion,
							 FechaNacimiento = item.EvaluadorNavigation.FechaNacimiento,
							 HoraRegistro = item.EvaluadorNavigation.HoraRegistro,

						 }


					});   

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaDiagnosticoDTO;

                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

		[HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<DiagnosticoDTO>();
            var diagnosticoDTO = new DiagnosticoDTO();

            try
            {

                var dbDiagnostico = await _dbContext.DiagnosticoPacientes.FirstOrDefaultAsync(x => x.IdDiagnostico == id);

                if (dbDiagnostico != null)

                {

                    diagnosticoDTO.IdDiagnostico = dbDiagnostico.IdDiagnostico;
                    diagnosticoDTO.IdNuevo = dbDiagnostico.IdNuevo;
                    diagnosticoDTO.MotivoConsulta = dbDiagnostico.MotivoConsulta;
                    diagnosticoDTO.AntecedentesPatologicos = dbDiagnostico.AntecedentesPatologicos;
                    diagnosticoDTO.EstiloVida = dbDiagnostico.EstiloVida;
                    diagnosticoDTO.Resultado = dbDiagnostico.Resultado;
                    diagnosticoDTO.Observaciones = dbDiagnostico.Observaciones;
                    diagnosticoDTO.Evaluador = dbDiagnostico.Evaluador;
                    diagnosticoDTO.FechaRegistro = dbDiagnostico.FechaRegistro;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = diagnosticoDTO;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro ningun Paciente";
                }



            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(DiagnosticoDTO diagnostico)
        {
            var responseApi = new ResponseAPI<int>();


            try
            {

                var dbDiagnostico = new DiagnosticoPaciente
                {
                    IdDiagnostico = diagnostico.IdDiagnostico,
                    IdNuevo = diagnostico.IdNuevo,
                    MotivoConsulta = diagnostico.MotivoConsulta,
                    AntecedentesPatologicos = diagnostico.AntecedentesPatologicos,
                    EstiloVida = diagnostico.EstiloVida,
                    Resultado = diagnostico.Resultado,
                    Observaciones = diagnostico.Observaciones,
                    Evaluador = diagnostico.Evaluador,
                    FechaRegistro = diagnostico.FechaRegistro,


            };

                _dbContext.DiagnosticoPacientes.Add(dbDiagnostico);
                await _dbContext.SaveChangesAsync();

                if (dbDiagnostico.IdDiagnostico != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbDiagnostico.IdDiagnostico;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se guardo el diagnostico";
                }



            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(DiagnosticoDTO diagnostico, int id)
        {
            var responseApi = new ResponseAPI<int>();


            try
            {

                var dbDiagnostico = await _dbContext.DiagnosticoPacientes.FirstOrDefaultAsync(e => e.IdDiagnostico == id);



                if (dbDiagnostico != null)
                {

                    dbDiagnostico.IdDiagnostico = diagnostico.IdDiagnostico;
                    dbDiagnostico.IdNuevo = diagnostico.IdNuevo;
                    dbDiagnostico.MotivoConsulta = diagnostico.MotivoConsulta;
                    dbDiagnostico.AntecedentesPatologicos = diagnostico.AntecedentesPatologicos;
                    dbDiagnostico.EstiloVida = diagnostico.EstiloVida;
                    dbDiagnostico.Resultado = diagnostico.Resultado;
                    dbDiagnostico.Observaciones = diagnostico.Observaciones;
                    dbDiagnostico.Evaluador = diagnostico.Evaluador;
                    dbDiagnostico.FechaRegistro = diagnostico.FechaRegistro;

                   
                    _dbContext.DiagnosticoPacientes.Update(dbDiagnostico);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbDiagnostico.IdDiagnostico;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el diagnostico";
                }



            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbDiagnostico = await _dbContext.DiagnosticoPacientes.FirstOrDefaultAsync(e => e.IdDiagnostico == id);

                if (dbDiagnostico != null)
                {
                    _dbContext.DiagnosticoPacientes.Remove(dbDiagnostico);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el Diagnostico";
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



	}
}
