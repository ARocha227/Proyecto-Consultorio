using ConsulCrud.Shared;
using System.Net.Http.Json;

namespace Blazor1.Crud.Client.Services
{
	public class PacienteService : IPacienteService
	{
		private readonly HttpClient _http;

		public PacienteService(HttpClient http)
		{
			_http = http;
		}

		public async Task<List<PacienteDTO>> Lista()
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<List<PacienteDTO>>>("api/Paciente/Lista");
			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}
		public async Task<PacienteDTO> Buscar(int id)
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<PacienteDTO>>($"api/Paciente/Buscar/{id}");
			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}
		public async Task<int> Guardar(PacienteDTO paciente)
		{
			var result = await _http.PostAsJsonAsync("api/Paciente/Guardar", paciente);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}
		public async Task<int> Editar(PacienteDTO paciente)
		{
			var result = await _http.PutAsJsonAsync($"api/Paciente/Editar/{paciente.IdNuevo}", paciente);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}

		public async Task<bool> Eliminar(int id)
		{
			var result = await _http.DeleteAsync($"api/Paciente/Eliminar/{id}");
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.EsCorrecto!;
			else
				throw new Exception(response.Mensaje);
		}



	}
}
