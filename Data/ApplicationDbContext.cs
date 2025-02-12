using BankApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data;

// ApplicationDbContext inherits from IdentityDbContext => provides Identity related
// functions and manage the database context for the app.
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<BankTransaction> BankTransactions { get; set; }
    public DbSet<Account> Accounts { get; set; }

    // Configure the model and relationships between entities.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<BankTransaction>()
        // .HasOne(t => t.FromAccount)  // One-to-Many => Reference Account entity
        // .WithMany(a => a.BankTransactions)  // One-to-Many => An Account has many Transactions
        // .HasForeignKey(t => t.FromAccountId)  // Defining FK "explicitly"
        // .OnDelete(DeleteBehavior.Restrict); // Feedback => use Cascade if needed(?)

        base.OnModelCreating(builder);
        // Code snippet from Master Max!
        // SQLite does not support nvarchar(max), convert to TEXT
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.GetColumnType() == "nvarchar(max)")
                    {
                        property.SetColumnType("TEXT");
                    }
                }
            }
        }
    }
}