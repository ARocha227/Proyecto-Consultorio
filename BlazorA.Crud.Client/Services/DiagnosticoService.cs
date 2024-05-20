using ConsulCrud.Shared;
using System.Net.Http.Json;

namespace Blazor1.Crud.Client.Services
{
	public class DiagnosticoService : IDiagnosticoService
	{
		private readonly HttpClient _http;

		public DiagnosticoService(HttpClient http)
		{
			_http = http;
		}
		public async Task<List<DiagnosticoDTO>> Lista()
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<List<DiagnosticoDTO>>>("api/Diagnostico/Lista");
			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}
		public async Task<DiagnosticoDTO> Buscar(int id)
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<DiagnosticoDTO>>($"api/Diagnostico/Buscar/{id}");
			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}

		public async Task<int> Guardar(DiagnosticoDTO diagnostico)
		{
			var result = await _http.PostAsJsonAsync("api/Diagnostico/Guardar", diagnostico);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}
		public async Task<int> Editar(DiagnosticoDTO diagnostico)
		{
			var result = await _http.PutAsJsonAsync($"api/Diagnostico/Editar/{diagnostico.IdDiagnostico}", diagnostico);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}

		public async Task<bool> Eliminar(int id)
		{
			var result = await _http.DeleteAsync($"api/Diagnostico/Eliminar/{id}");
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.EsCorrecto!;
			else
				throw new Exception(response.Mensaje);
		}
	}
}
