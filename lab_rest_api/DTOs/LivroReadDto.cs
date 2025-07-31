namespace Laboratorio_rest_api.DTOs;

public class LivroReadDto
{
    public int LivroId { get; set; }
    public required string Titulo { get; set; }
    public required string AutorNomeCompleto { get; set; }
}
