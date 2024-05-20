using ConsulCrud.Shared;
using System.Net.Http.Json;

namespace Blazor1.Crud.Client.Services
{
	public class DepartamentoService : IDepartamentoService
	{
		private readonly HttpClient _http;

		public DepartamentoService(HttpClient http)
		{
			_http = http;
		}

		public async Task<List<DepartamentoDTO>> Lista()
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<List<DepartamentoDTO>>>("api/Departamento/Lista");
			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}

		public async Task<DepartamentoDTO> Buscar(int id)
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<DepartamentoDTO>>($"api/Departamento/Buscar/{id}");
			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}
		public async Task<int> Guardar(DepartamentoDTO departamento)
		{
			var result = await _http.PostAsJsonAsync("api/Departamento/Guardar", departamento);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}
		public async Task<int> Editar(DepartamentoDTO departamento)
		{
			var result = await _http.PutAsJsonAsync($"api/Departamento/Editar/{departamento.Iddepartamento}", departamento);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}

		public async Task<bool> Eliminar(int id)
		{
			var result = await _http.DeleteAsync($"api/Departamento/Eliminar/{id}");
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.EsCorrecto!;
			else
				throw new Exception(response.Mensaje);
		}
	}
}
