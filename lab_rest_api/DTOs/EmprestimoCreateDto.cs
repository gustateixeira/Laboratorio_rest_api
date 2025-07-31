namespace Laboratorio_rest_api.DTOs;

public class EmprestimoCreateDto
{
    public required int LivroId { get; set; }
    public required DateTime DataRetirada { get; set; }
    public required DateTime DataDevolucao { get; set; }
}
