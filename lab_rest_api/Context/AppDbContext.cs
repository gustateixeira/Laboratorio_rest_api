namespace Laboratorio_rest_api;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }
}