using Laboratorio_rest_api.Repositories;

namespace Laboratorio_rest_api.Service;

public class BibliotecaService
{
    private readonly ILivroRepository _livroRepo;
    private readonly IEmprestimoRepository _emprestimoRepo;

    private const decimal MultaPorDia = 2.50m; 

    public BibliotecaService(ILivroRepository livroRepo, IEmprestimoRepository emprestimoRepo)
    {
        _livroRepo = livroRepo;
        _emprestimoRepo = emprestimoRepo;
    }

    public async Task<(bool sucesso, string mensagem)> EmprestarLivroAsync(int livroId)
    {
        var livro = await _livroRepo.ObterPorIdAsync(livroId);
        if (livro == null)
            return (false, "Livro não encontrado.");

        var emprestimoAberto = await _emprestimoRepo.ObterEmprestimoAbertoPorLivroIdAsync(livroId);
        if (emprestimoAberto != null)
            return (false, "Livro indisponível para empréstimo.");

        var novoEmprestimo = new Emprestimo
        {
            LivroId = livroId,
            DataRetirada = DateTime.Now,
            DataDevolucao = DateTime.Now.AddDays(7),
            Entregue = false
        };

        await _emprestimoRepo.RegistrarAsync(novoEmprestimo);
        return (true, "Empréstimo registrado com sucesso.");
    }

    public async Task<(bool sucesso, decimal multa, string mensagem)> DevolverLivroAsync(int livroId)
    {
        var emprestimo = await _emprestimoRepo.ObterEmprestimoAbertoPorLivroIdAsync(livroId);
        if (emprestimo == null)
            return (false, 0, "Nenhum empréstimo em aberto encontrado para este livro.");

        emprestimo.Entregue = true;
        var hoje = DateTime.Now;
        var diasAtraso = (hoje - emprestimo.DataDevolucao).Days;

        decimal multa = diasAtraso > 0 ? diasAtraso * MultaPorDia : 0;

        await _emprestimoRepo.AlterarAsync(emprestimo);
        return (true, multa, "Livro devolvido com sucesso.");
    }

    public async Task<IEnumerable<(Livro livro, bool disponivel, DateTime? dataDevolucao)>> ConsultarLivrosPorAutorAsync(int autorId)
    {
        var livros = await _livroRepo.ObterPorAutorIdAsync(autorId);

        var resultado = new List<(Livro, bool, DateTime?)>();

        foreach (var livro in livros)
        {
            var emprestimo = await _emprestimoRepo.ObterEmprestimoAbertoPorLivroIdAsync(livro.LivroId);

            if (emprestimo == null)
            {
                resultado.Add((livro, true, null));
            }
            else
            {
                resultado.Add((livro, false, emprestimo.DataDevolucao));
            }
        }

        return resultado;
    }
}
