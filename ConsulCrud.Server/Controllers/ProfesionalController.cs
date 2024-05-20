using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ConsulCrud.Server.Model;
using ConsulCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace ConsulCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalController : ControllerBase
    {
        private readonly PacientesBdContext _dbContext;

        public ProfesionalController(PacientesBdContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<ProfesionalDTO>>();
            var listaProfesionalDTO = new List<ProfesionalDTO>();

            try
            {
                foreach (var item in await _dbContext.Profesionals.ToListAsync())
                {
                    listaProfesionalDTO.Add(new ProfesionalDTO
                    {
                        IdCodigoP = item.IdCodigoP,
                        IdIdentificador = item.IdIdentificador,
                        NombreCompleto = item.NombreCompleto,
                        CorreoElectronico = item.CorreoElectronico,
                        Telefono = item.Telefono,
                        CdigoProfesional = item.CdigoProfesional,
                        Direccion = item.Direccion,
                        FechaNacimiento = item.FechaNacimiento,
                        HoraRegistro = item.HoraRegistro,

                     
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaProfesionalDTO;

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
            var responseApi = new ResponseAPI<ProfesionalDTO>();
            var profesionalDTO = new ProfesionalDTO();

            try
            {

                var dbProfesional = await _dbContext.Profesionals.FirstOrDefaultAsync(x => x.IdCodigoP == id);

                if (dbProfesional != null)

                {
                   
                    profesionalDTO.IdCodigoP = dbProfesional.IdCodigoP;
                    profesionalDTO.IdIdentificador = dbProfesional.IdIdentificador;
                    profesionalDTO.NombreCompleto = dbProfesional.NombreCompleto;
                    profesionalDTO.CorreoElectronico = dbProfesional.CorreoElectronico;
                    profesionalDTO.Telefono = dbProfesional.Telefono;
                    profesionalDTO.CdigoProfesional = dbProfesional.CdigoProfesional;
                    profesionalDTO.Direccion = dbProfesional.Direccion;
                    profesionalDTO.FechaNacimiento = dbProfesional.FechaNacimiento;
                    profesionalDTO.HoraRegistro = dbProfesional.HoraRegistro;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = profesionalDTO;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro ningun Profesional";
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
        public async Task<IActionResult> Guardar(ProfesionalDTO profesional)
        {
            var responseApi = new ResponseAPI<int>();


            try
            {

                var dbProfesional = new Profesional
                {
                    IdCodigoP = profesional.IdCodigoP,
                    IdIdentificador = profesional.IdIdentificador,
                    NombreCompleto = profesional.NombreCompleto,
                    CorreoElectronico = profesional.CorreoElectronico,
                    Telefono = profesional.Telefono,
                    CdigoProfesional = profesional.CdigoProfesional,
                    Direccion = profesional.Direccion,
                    FechaNacimiento = profesional.FechaNacimiento,
                    HoraRegistro = profesional.HoraRegistro,
               

            };

                _dbContext.Profesionals.Add(dbProfesional);
                await _dbContext.SaveChangesAsync();

                if (dbProfesional.IdCodigoP != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProfesional.IdCodigoP;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se guardo los datos del Profesional";
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
        public async Task<IActionResult> Editar(ProfesionalDTO profesional, int id)
        {
            var responseApi = new ResponseAPI<int>();


            try
            {

                var dbProfesional = await _dbContext.Profesionals.FirstOrDefaultAsync(e => e.IdCodigoP == id);



                if (dbProfesional != null)
                {

                    dbProfesional.IdCodigoP = profesional.IdCodigoP;
                    dbProfesional.IdIdentificador = profesional.IdIdentificador;
                    dbProfesional.NombreCompleto = profesional.NombreCompleto;
                    dbProfesional.CorreoElectronico = profesional.CorreoElectronico;
                    dbProfesional.Telefono = profesional.Telefono;
                    dbProfesional.IdIdentificador = profesional.IdIdentificador;
                    dbProfesional.IdCodigoP = profesional.IdCodigoP;
                    dbProfesional.CdigoProfesional = profesional.CdigoProfesional;
                    dbProfesional.Direccion = profesional.Direccion;
                    dbProfesional.FechaNacimiento = profesional.FechaNacimiento;
                    dbProfesional.HoraRegistro = profesional.HoraRegistro;


                    _dbContext.Profesionals.Update(dbProfesional);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProfesional.IdCodigoP;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el Profesional";
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

                var dbProfesional = await _dbContext.Profesionals.FirstOrDefaultAsync(e => e.IdCodigoP == id);

                if (dbProfesional != null)
                {
                    _dbContext.Profesionals.Remove(dbProfesional);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el Profesional";
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
