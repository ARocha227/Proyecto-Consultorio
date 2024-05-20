using ConsulCrud.Shared;
using System.Net.Http.Json;

namespace Blazor1.Crud.Client.Services
{
	public class ProfesionalService : IProfesionalService
	{
		private readonly HttpClient _http;

		public ProfesionalService(HttpClient http)
		{
			_http = http;
		}
		public async Task<List<ProfesionalDTO>> Lista()
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<List<ProfesionalDTO>>>("api/Profesional/Lista");
			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}
		public async Task<ProfesionalDTO> Buscar(int id)
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<ProfesionalDTO>>($"api/Profesional/Buscar/{id}");
			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}
		public async Task<int> Guardar(ProfesionalDTO profesional)
		{
			var result = await _http.PostAsJsonAsync("api/Profesional/Guardar", profesional);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}
		public async Task<int> Editar(ProfesionalDTO profesional)
		{
			var result = await _http.PutAsJsonAsync($"api/Profesional/Editar/{profesional.IdCodigoP}", profesional);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}

		public async Task<bool> Eliminar(int id)
		{
			var result = await _http.DeleteAsync($"api/Profesional/Eliminar/{id}");
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.EsCorrecto!;
			else
				throw new Exception(response.Mensaje);
		}




	}
}
