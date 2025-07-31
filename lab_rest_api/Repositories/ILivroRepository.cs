namespace Laboratorio_rest_api.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILivroRepository
{
    Task RegistrarAsync(Livro livro);
    Task<IEnumerable<Livro>> ObterTodosAsync();
    Task<IEnumerable<Livro>> ObterPorAutorIdAsync(int autorId);
    Task<Livro?> ObterPorIdAsync(int livroId); // útil para controller
}
