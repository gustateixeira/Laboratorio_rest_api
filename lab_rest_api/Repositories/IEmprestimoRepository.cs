namespace Laboratorio_rest_api.Repositories;

using System.Threading.Tasks;

public interface IEmprestimoRepository
{
    Task RegistrarAsync(Emprestimo emprestimo);
    Task AlterarAsync(Emprestimo emprestimo);
    Task<Emprestimo?> ObterEmprestimoAbertoPorLivroIdAsync(int livroId);
    Task<Emprestimo?> ObterPorIdAsync(int id);
}
