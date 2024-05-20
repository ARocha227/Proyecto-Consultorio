using ConsulCrud.Shared;

namespace Blazor1.Crud.Client.Services
{
	public interface IPacienteService
	{
		Task<List<PacienteDTO>> Lista();
		Task<PacienteDTO> Buscar(int id);
		Task<int> Guardar(PacienteDTO paciente);
		Task<int> Editar(PacienteDTO paciente);
		Task<bool> Eliminar(int id);

	}
}
