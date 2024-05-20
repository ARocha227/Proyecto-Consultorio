using ConsulCrud.Shared;

namespace Blazor1.Crud.Client.Services
{
	public interface IProfesionalService
	{
		Task<List<ProfesionalDTO>> Lista();
		Task<ProfesionalDTO> Buscar(int id);
		Task<int> Guardar(ProfesionalDTO profesional);
		Task<int> Editar(ProfesionalDTO profesional);
		Task<bool> Eliminar(int id);
	}
}
