using ConsulCrud.Shared;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorA.Crud.Client.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public AutenticacionExtension(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task ActualizarEstadoAutenticacion (SesionDTO? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                { 
                    new Claim(ClaimTypes.Name,sesionUsuario.nombre),
                     new Claim(ClaimTypes.Email,sesionUsuario.correo),
                      new Claim(ClaimTypes.Role,sesionUsuario.rol),
                      

                }, "JwtAuth"));

                await _sessionStorage.GuardarStorage("SesionUsuario", sesionUsuario);

            } else
            {
                claimsPrincipal = _sinInformacion;
                await _sessionStorage.RemoveItemAsync("SesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _sessionStorage.ObtenerStorage<SesionDTO>("SesionUsuario");
            if (sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_sinInformacion));

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.nombre),
                     new Claim(ClaimTypes.Email,sesionUsuario.correo),
                      new Claim(ClaimTypes.Role,sesionUsuario.rol),


                }, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        } 
    }
}
