using ConsulCrud.Shared;

namespace Blazor1.Crud.Client.Services
{
	public interface IDiagnosticoService
	{
		Task<List<DiagnosticoDTO>> Lista();
		Task<DiagnosticoDTO> Buscar(int id);
		Task<int> Guardar(DiagnosticoDTO diagnostico);
		Task<int> Editar(DiagnosticoDTO diagnostico);
		Task<bool> Eliminar(int id);



	}
}
