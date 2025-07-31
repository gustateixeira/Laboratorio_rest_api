
public class Autor
{
    public int AutorId { get; set; }
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
    public ICollection<Livro> Livros { get; set; }
}