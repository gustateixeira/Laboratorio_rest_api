using Laboratorio_rest_api.DTOs;
using Laboratorio_rest_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio_rest_api.Controller;

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroRepository _livroRepo;

    public LivroController(ILivroRepository livroRepo)
    {
        _livroRepo = livroRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CriarLivro([FromBody] LivroCreateDto dto)
    {
        var livro = new Livro
        {
            Titulo = dto.Titulo,
            AutorId = dto.AutorId
        };

        await _livroRepo.RegistrarAsync(livro);

        var livroRead = new LivroReadDto
        {
            LivroId = livro.LivroId,
            Titulo = livro.Titulo,
            AutorNomeCompleto = "" // opcional: você pode recuperar o autor se quiser
        };

        return CreatedAtAction(nameof(ObterPorId), new { id = livro.LivroId }, livroRead);
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var livros = await _livroRepo.ObterTodosAsync();

        var resultado = livros.Select(l => new LivroReadDto
        {
            LivroId = l.LivroId,
            Titulo = l.Titulo,
            AutorNomeCompleto = $"{l.Autor.PrimeiroNome} {l.Autor.UltimoNome}"
        });

        return Ok(resultado);
    }

    [HttpGet("autor/{autorId}")]
    public async Task<IActionResult> ObterPorAutor(int autorId)
    {
        var livros = await _livroRepo.ObterPorAutorIdAsync(autorId);

        var resultado = livros.Select(l => new LivroReadDto
        {
            LivroId = l.LivroId,
            Titulo = l.Titulo,
            AutorNomeCompleto = $"{l.Autor.PrimeiroNome} {l.Autor.UltimoNome}"
        });

        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var livro = await _livroRepo.ObterPorIdAsync(id);
        if (livro == null) return NotFound();

        var livroDto = new LivroReadDto
        {
            LivroId = livro.LivroId,
            Titulo = livro.Titulo,
            AutorNomeCompleto = $"{livro.Autor.PrimeiroNome} {livro.Autor.UltimoNome}"
        };

        return Ok(livroDto);
    }
}
