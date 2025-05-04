using Microsoft.EntityFrameworkCore;
using TourFlexSystem.Domain.Models;

namespace TourFlexSystem.Domain;

public class TourFlexDbContext : DbContext
{
    public TourFlexDbContext(DbContextOptions<TourFlexDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<JournalledTransaction> JournalledTransactions { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public DbSet<ExpenseParticipant> ExpenseParticipants { get; set; }

    public DbSet<Friend> Friends { get; set; }

    public DbSet<SplitPayment> SplitPayments { get; set; }

    public DbSet<SplitPaymentLog> SplitPaymentLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TourFlexDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
