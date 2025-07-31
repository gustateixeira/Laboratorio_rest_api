using Laboratorio_rest_api.Service;

namespace Laboratorio_rest_api.Controller;


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BibliotecaController : ControllerBase
{
    private readonly BibliotecaService _service;

    public BibliotecaController(BibliotecaService service)
    {
        _service = service;
    }

    [HttpPost("emprestar/{livroId}")]
    public async Task<IActionResult> EmprestarLivro(int livroId)
    {
        var (sucesso, mensagem) = await _service.EmprestarLivroAsync(livroId);
        if (!sucesso) return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }

    [HttpPost("devolver/{livroId}")]
    public async Task<IActionResult> DevolverLivro(int livroId)
    {
        var (sucesso, multa, mensagem) = await _service.DevolverLivroAsync(livroId);
        if (!sucesso) return BadRequest(new { mensagem });
        return Ok(new { mensagem, multa });
    }

    [HttpGet("livros/autor/{autorId}")]
    public async Task<IActionResult> ConsultarLivrosPorAutor(int autorId)
    {
        var resultado = await _service.ConsultarLivrosPorAutorAsync(autorId);

        var response = resultado.Select(r => new
        {
            LivroId = r.livro.LivroId,
            Titulo = r.livro.Titulo,
            Disponivel = r.disponivel,
            DataDevolucaoPrevista = r.dataDevolucao
        });

        return Ok(response);
    }
}
