using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Data;

/// <summary>
/// Контекст базы данных.
/// DbSet&lt;Contact&gt; — таблица контактов.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>Таблица контактов.</summary>
    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>(e =>
        {
            e.HasKey(c => c.Id);

            e.Property(c => c.Name)
             .IsRequired()
             .HasMaxLength(100);

            e.Property(c => c.PhoneNumber)
             .IsRequired()
             .HasMaxLength(30);

            e.Property(c => c.Email)
             .HasMaxLength(150);
        });
    }
}
