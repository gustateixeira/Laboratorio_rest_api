using Laboratorio_rest_api.DTOs;
using Laboratorio_rest_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio_rest_api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly IAutorRepository _autorRepo;

    public AutorController(IAutorRepository autorRepo)
    {
        _autorRepo = autorRepo;
    }

    [HttpGet("buscar-por-sobrenome/{sobrenome}")]
    public async Task<IActionResult> BuscarPorSobrenome(string sobrenome)
    {
        var autores = await _autorRepo.BuscarPorUltimoNomeAsync(sobrenome);

        var resultado = autores.Select(a => new AutorReadDto
        {
            AutorId = a.AutorId,
            NomeCompleto = $"{a.PrimeiroNome} {a.UltimoNome}"
        });

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> CriarAutor([FromBody] AutorCreateDto dto)
    {
        var autor = new Autor
        {
            PrimeiroNome = dto.PrimeiroNome,
            UltimoNome = dto.UltimoNome
        };

        await _autorRepo.RegistrarAsync(autor);

        var autorRead = new AutorReadDto
        {
            AutorId = autor.AutorId,
            NomeCompleto = $"{autor.PrimeiroNome} {autor.UltimoNome}"
        };

        return CreatedAtAction(nameof(BuscarPorSobrenome), new { sobrenome = autor.UltimoNome }, autorRead);
    }
}
