using Laboratorio_rest_api.DTOs;
using Laboratorio_rest_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio_rest_api.Controller;

[ApiController]
[Route("api/[controller]")]
public class EmprestimoController : ControllerBase
{
    private readonly IEmprestimoRepository _emprestimoRepo;

    public EmprestimoController(IEmprestimoRepository emprestimoRepo)
    {
        _emprestimoRepo = emprestimoRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CriarEmprestimo([FromBody] EmprestimoCreateDto dto)
    {
        var emprestimo = new Emprestimo
        {
            LivroId = dto.LivroId,
            DataRetirada = dto.DataRetirada,
            DataDevolucao = dto.DataDevolucao,
            Entregue = false
        };

        await _emprestimoRepo.RegistrarAsync(emprestimo);

        var emprestimoRead = new EmprestimoReadDto
        {
            EmprestimoId = emprestimo.EmprestimoId,
            TituloLivro = "", // opcional
            DataRetirada = emprestimo.DataRetirada,
            DataDevolucao = emprestimo.DataDevolucao,
            Entregue = emprestimo.Entregue
        };

        return CreatedAtAction(nameof(ObterEmprestimoAberto), new { livroId = dto.LivroId }, emprestimoRead);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarEmprestimo(int id, [FromBody] EmprestimoCreateDto dto)
    {
        var emprestimo = await _emprestimoRepo.ObterPorIdAsync(id);
        if (emprestimo == null)
            return NotFound();

        emprestimo.DataRetirada = dto.DataRetirada;
        emprestimo.DataDevolucao = dto.DataDevolucao;

        await _emprestimoRepo.AlterarAsync(emprestimo);

        return NoContent();
    }

    [HttpGet("em-aberto/livro/{livroId}")]
    public async Task<IActionResult> ObterEmprestimoAberto(int livroId)
    {
        var emprestimo = await _emprestimoRepo.ObterEmprestimoAbertoPorLivroIdAsync(livroId);
        if (emprestimo == null) return NotFound();

        var emprestimoDto = new EmprestimoReadDto
        {
            EmprestimoId = emprestimo.EmprestimoId,
            TituloLivro = emprestimo.Livro?.Titulo ?? "",
            DataRetirada = emprestimo.DataRetirada,
            DataDevolucao = emprestimo.DataDevolucao,
            Entregue = emprestimo.Entregue
        };

        return Ok(emprestimoDto);
    }
}
