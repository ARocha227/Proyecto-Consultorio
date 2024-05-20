using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ConsulCrud.Server.Model;
using ConsulCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace ConsulCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PacientesBdContext _dbContext;

        public PacienteController(PacientesBdContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<PacienteDTO>>();
            var listaPacienteDTO = new List<PacienteDTO>();

            try
            {
                foreach (var item in await _dbContext.Pacientes.Include(d => d.IddepartamentoNavigation).ToListAsync())
                {
                    listaPacienteDTO.Add(new PacienteDTO
                    {
                        IdNuevo = item.IdNuevo,
                        IdPaciente = item.IdPaciente,
                        NombreCompleto = item.NombreCompleto,
                        Iddepartamento = item.Iddepartamento,
                        CorreoElectronico = item.CorreoElectronico,
                        Telefono = item.Telefono,
                        Direccion = item.Direccion,
                        EstadoCivil = item.EstadoCivil,
                        Nacionalidad = item.Nacionalidad,
                        FechaNacimiento = item.FechaNacimiento,
                        HoraRegistro = item.HoraRegistro,

                        Departamento = new DepartamentoDTO
                        {
                            Iddepartamento = item.IddepartamentoNavigation.Iddepartamento,
                            Nombre = item.IddepartamentoNavigation.Nombre
                        }

                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaPacienteDTO;

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
        public async Task<IActionResult> Buscar( int id)
        {
            var responseApi = new ResponseAPI<PacienteDTO>();
            var pacienteDTO = new PacienteDTO();

            try
            {

                var dbPaciente = await _dbContext.Pacientes.FirstOrDefaultAsync(x => x.IdNuevo == id);

                if (dbPaciente != null)

                {
                    pacienteDTO.IdNuevo = dbPaciente.IdNuevo;
                    pacienteDTO.IdPaciente = dbPaciente.IdPaciente;
                    pacienteDTO.NombreCompleto = dbPaciente.NombreCompleto;
                    pacienteDTO.Iddepartamento = dbPaciente.Iddepartamento;
                    pacienteDTO.CorreoElectronico = dbPaciente.CorreoElectronico;
                    pacienteDTO.Telefono = dbPaciente.Telefono;
                    pacienteDTO.Direccion = dbPaciente.Direccion;
                    pacienteDTO.EstadoCivil = dbPaciente.EstadoCivil;
                    pacienteDTO.Nacionalidad = dbPaciente.Nacionalidad;
                    pacienteDTO.FechaNacimiento = dbPaciente.FechaNacimiento;
                    pacienteDTO.HoraRegistro = dbPaciente.HoraRegistro;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = pacienteDTO;


                } else
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
        public async Task<IActionResult> Guardar(PacienteDTO paciente)
        {
            var responseApi = new ResponseAPI<int>();
            

            try
            {

                var dbPaciente = new Paciente
                {
                    IdPaciente = paciente.IdPaciente,
                    NombreCompleto = paciente.NombreCompleto,
                    Iddepartamento = paciente.Iddepartamento,
                    CorreoElectronico = paciente.CorreoElectronico,
                    Telefono = paciente.Telefono,
                    Direccion = paciente.Direccion,
                    EstadoCivil = paciente.EstadoCivil,
                    Nacionalidad = paciente.Nacionalidad,
                    FechaNacimiento = paciente.FechaNacimiento,
                    HoraRegistro = paciente.HoraRegistro,

                };

                _dbContext.Pacientes.Add(dbPaciente);
                await _dbContext.SaveChangesAsync();

                if (dbPaciente.IdNuevo != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbPaciente.IdPaciente;
                } else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se guardo el paciente";
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
        public async Task<IActionResult> Editar (PacienteDTO paciente, int id)
        {
            var responseApi = new ResponseAPI<int>();


            try
            {

                var dbPaciente = await _dbContext.Pacientes.FirstOrDefaultAsync(e => e.IdNuevo == id);



                if (dbPaciente != null)
                {

                    dbPaciente.IdPaciente = paciente.IdPaciente;
                    dbPaciente.NombreCompleto = paciente.NombreCompleto;
                    dbPaciente.Iddepartamento = paciente.Iddepartamento;
                    dbPaciente.CorreoElectronico = paciente.CorreoElectronico;
                    dbPaciente.Telefono = paciente.Telefono;
                    dbPaciente.Direccion = paciente.Direccion;
                    dbPaciente.EstadoCivil = paciente.EstadoCivil;
                    dbPaciente.Nacionalidad = paciente.Nacionalidad;
                    dbPaciente.FechaNacimiento = paciente.FechaNacimiento;
                    dbPaciente.HoraRegistro = paciente.HoraRegistro;

                    _dbContext.Pacientes.Update(dbPaciente);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbPaciente.IdPaciente;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el paciente";
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

                var dbPaciente = await _dbContext.Pacientes.FirstOrDefaultAsync(e => e.IdNuevo == id);

                if (dbPaciente != null)
                {           
                    _dbContext.Pacientes.Remove(dbPaciente);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el paciente";
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
