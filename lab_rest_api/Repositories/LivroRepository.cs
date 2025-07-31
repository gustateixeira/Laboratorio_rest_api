namespace Laboratorio_rest_api.Repositories;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class LivroRepository : ILivroRepository
{
    private readonly AppDbContext _context;

    public LivroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarAsync(Livro livro)
    {
        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Livro>> ObterTodosAsync()
    {
        return await _context.Livros
            .Include(l => l.Autor)
            .ToListAsync();
    }

    public async Task<IEnumerable<Livro>> ObterPorAutorIdAsync(int autorId)
    {
        return await _context.Livros
            .Where(l => l.AutorId == autorId)
            .Include(l => l.Autor)
            .ToListAsync();
    }

    public async Task<Livro?> ObterPorIdAsync(int livroId)
    {
        return await _context.Livros
            .Include(l => l.Autor)
            .FirstOrDefaultAsync(l => l.LivroId == livroId);
    }
}
