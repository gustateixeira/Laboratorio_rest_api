namespace Laboratorio_rest_api;

public class Emprestimo
{
    public int EmprestimoId { get; set; }
    public DateTime DataRetirada { get; set; }
    public DateTime DataDevolucao { get; set; }
    public bool entregue { get; set; }
}