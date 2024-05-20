using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsulCrud.Shared;

namespace ConsulCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task <IActionResult> Login([FromBody] LogginDTO login) 
        {
            SesionDTO sesionDTO = new SesionDTO();
            if (login.correo == "marigsm27@gmail.com" && login.clave == "77ARochaGM22")
            {
                sesionDTO.nombre = "Lic.Guadalupe Mendoza Sanchez";
                sesionDTO.correo = login.correo;
                sesionDTO.rol = "Administrador";

            }
            else
            {
                sesionDTO.nombre = "Asistente";
                sesionDTO.correo = login.correo;
                sesionDTO.rol = "Asistente";
            }
            return StatusCode(StatusCodes.Status200OK,sesionDTO);
        }
    }
}
