namespace Laboratorio_rest_api.Repositories;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class AutorRepository : IAutorRepository
{
    private readonly AppDbContext _context;

    public AutorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Autor>> BuscarPorUltimoNomeAsync(string ultimoNome)
    {
        return await _context.Autores
            .Where(a => a.UltimoNome == ultimoNome)
            .ToListAsync();
    }

    public async Task RegistrarAsync(Autor autor)
    {
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
    }

    public async Task AlterarAsync(Autor autor)
    {
        _context.Autores.Update(autor);
        await _context.SaveChangesAsync();
    }
}
