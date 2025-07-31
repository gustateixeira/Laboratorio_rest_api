namespace Laboratorio_rest_api.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAutorRepository
{
    Task<IEnumerable<Autor>> BuscarPorUltimoNomeAsync(string ultimoNome);
    Task RegistrarAsync(Autor autor);
    Task AlterarAsync(Autor autor);
}
