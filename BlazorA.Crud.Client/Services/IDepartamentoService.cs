using ConsulCrud.Shared;


namespace Blazor1.Crud.Client.Services
{
	public interface IDepartamentoService
	{
		Task<List<DepartamentoDTO>> Lista();

		Task<DepartamentoDTO> Buscar(int id);
		Task<int> Guardar(DepartamentoDTO departamento);
		Task<int> Editar(DepartamentoDTO departamento);
		Task<bool> Eliminar(int id);
	}
}
