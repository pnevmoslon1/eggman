using DataBase.Models;
using Microsoft.EntityFrameworkCore;


namespace DataBase;

public class ExamDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }

    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("", o => o.SetPostgresVersion(12, 2));
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Article>()
            .HasIndex(x => x.Id)
            .IsUnique();
        modelBuilder.Entity<Article>()
            .HasMany(x => x.Authors)
            .WithOne(x => x.Article)
            .HasForeignKey(x => x.ArticleId);
        modelBuilder.Entity<Author>()
            .HasIndex(x => x.Id)
            .IsUnique();
        modelBuilder.Entity<Author>()
            .HasOne(x => x.Article)
            .WithMany(x => x.Authors)
            .HasForeignKey(x => x.ArticleId);
    }
}