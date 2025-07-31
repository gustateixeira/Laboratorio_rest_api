namespace Laboratorio_rest_api.Repositories;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly AppDbContext _context;

    public EmprestimoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarAsync(Emprestimo emprestimo)
    {
        _context.Emprestimos.Add(emprestimo);
        await _context.SaveChangesAsync();
    }

    public async Task AlterarAsync(Emprestimo emprestimo)
    {
        _context.Emprestimos.Update(emprestimo);
        await _context.SaveChangesAsync();
    }

    public async Task<Emprestimo?> ObterEmprestimoAbertoPorLivroIdAsync(int livroId)
    {
        return await _context.Emprestimos
            .Include(e => e.Livro)
            .FirstOrDefaultAsync(e => e.LivroId == livroId && !e.Entregue);
    }
    
    public async Task<Emprestimo?> ObterPorIdAsync(int id)
    {
        return await _context.Emprestimos
            .Include(e => e.Livro)
            .FirstOrDefaultAsync(e => e.EmprestimoId == id);
    }

}
