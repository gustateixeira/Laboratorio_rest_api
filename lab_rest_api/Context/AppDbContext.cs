using Microsoft.EntityFrameworkCore;

namespace Laboratorio_rest_api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Autor
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.HasKey(a => a.AutorId);

                entity.Property(a => a.PrimeiroNome)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(a => a.UltimoNome)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(a => a.Livros)
                      .WithOne(l => l.Autor)
                      .HasForeignKey(l => l.AutorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Livro
            modelBuilder.Entity<Livro>(entity =>
            {
                entity.HasKey(l => l.LivroId);

                entity.Property(l => l.Titulo)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasMany(l => l.Emprestimos)
                      .WithOne(e => e.Livro)
                      .HasForeignKey(e => e.LivroId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Empréstimo
            modelBuilder.Entity<Emprestimo>(entity =>
            {
                entity.HasKey(e => e.EmprestimoId);

                entity.Property(e => e.DataRetirada)
                      .IsRequired();

                entity.Property(e => e.DataDevolucao)
                      .IsRequired();

                entity.Property(e => e.Entregue)
                      .IsRequired();
            });
        }
    }
}
