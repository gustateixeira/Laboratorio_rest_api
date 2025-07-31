public class Livro
{
    public int LivroId { get; set; }
    public string Titulo { get; set; }
    public int AutorId { get; set; }
    public Autor Autor { get; set; }
    public ICollection<Emprestimo> Emprestimos { get; set; }
}