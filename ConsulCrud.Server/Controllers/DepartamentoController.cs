using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ConsulCrud.Server.Model;
using ConsulCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace ConsulCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        private readonly PacientesBdContext _dbContext;

        public DepartamentoController(PacientesBdContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<DepartamentoDTO>>();
            var listaDepartamentoDTO = new List<DepartamentoDTO>();

            try
            {
                foreach(var item in await _dbContext.Departamentos.ToListAsync())
                {
                    listaDepartamentoDTO.Add(new DepartamentoDTO 
                    { 
                    
                        Iddepartamento = item.Iddepartamento,
                        Nombre = item.Nombre,

                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaDepartamentoDTO;

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
			var responseApi = new ResponseAPI<DepartamentoDTO>();
			var departamentoDTO = new DepartamentoDTO();

			try
			{

				var dbdepartamento = await _dbContext.Departamentos.FirstOrDefaultAsync(x => x.Iddepartamento == id);

				if (dbdepartamento != null)

				{
					departamentoDTO.Iddepartamento = dbdepartamento.Iddepartamento;
					departamentoDTO.Nombre = dbdepartamento.Nombre;


					responseApi.EsCorrecto = true;
					responseApi.Valor = departamentoDTO;


				}
				else
				{
					responseApi.EsCorrecto = false;
					responseApi.Mensaje = "No se encontro ningun Departamento";
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
		public async Task<IActionResult> Guardar(DepartamentoDTO departamento)
		{
			var responseApi = new ResponseAPI<int>();


			try
			{

				var dbdepartamento = new Departamento
				{
					
					Nombre = departamento.Nombre,
					

				};

				_dbContext.Departamentos.Add(dbdepartamento);
				await _dbContext.SaveChangesAsync();

				if (dbdepartamento.Iddepartamento != 0)
				{
					responseApi.EsCorrecto = true;
					responseApi.Valor = dbdepartamento.Iddepartamento;
				}
				else
				{
					responseApi.EsCorrecto = false;
					responseApi.Mensaje = "No se guardo el Departamento";
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
		public async Task<IActionResult> Editar(DepartamentoDTO departamento, int id)
		{
			var responseApi = new ResponseAPI<int>();


			try
			{

				var dbdepartamento = await _dbContext.Departamentos.FirstOrDefaultAsync(e => e.Iddepartamento == id);



				if (dbdepartamento != null)
				{
					
					dbdepartamento.Nombre = departamento.Nombre;
					

					_dbContext.Departamentos.Update(dbdepartamento);
					await _dbContext.SaveChangesAsync();

					responseApi.EsCorrecto = true;
					responseApi.Valor = dbdepartamento.Iddepartamento;


				}
				else
				{
					responseApi.EsCorrecto = false;
					responseApi.Mensaje = "No se encontro el Departamento";
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

				var dbdepartamento = await _dbContext.Departamentos.FirstOrDefaultAsync(e => e.Iddepartamento == id);

				if (dbdepartamento != null)
				{
					_dbContext.Departamentos.Remove(dbdepartamento);
					await _dbContext.SaveChangesAsync();

					responseApi.EsCorrecto = true;

				}
				else
				{
					responseApi.EsCorrecto = false;
					responseApi.Mensaje = "No se encontro el Departamento";
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
