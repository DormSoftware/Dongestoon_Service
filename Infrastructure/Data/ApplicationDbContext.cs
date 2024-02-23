using Domain.Entities;
using Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Expense> Expense { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<User>()
        //     .HasMany(e => e.Groups)
        //     .WithMany(e => e.Users);
        modelBuilder.Entity<Product>()
            .HasOne(e => e.Category);


        // modelBuilder.Entity<Group>()
        //     .HasMany(e => e.Expenses)
        //     .WithOne(e => e.Group)
        //     .HasForeignKey("GroupId")
        //     .IsRequired();


        base.OnModelCreating(modelBuilder);
    }
}