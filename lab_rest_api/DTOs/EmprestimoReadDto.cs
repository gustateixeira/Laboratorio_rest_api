namespace Laboratorio_rest_api.DTOs;

public class EmprestimoReadDto
{
    public required int EmprestimoId { get; set; }
    public required string TituloLivro { get; set; }
    public required DateTime DataRetirada { get; set; }
    public required DateTime DataDevolucao { get; set; }
    public required bool Entregue { get; set; }
}
