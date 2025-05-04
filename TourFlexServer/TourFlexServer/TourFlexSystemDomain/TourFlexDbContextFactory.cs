using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TourFlexSystem.Domain;

/// <summary>
/// This is needed only to generate the migration scripts since this is a class library
/// https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
/// </summary>
public class TourFlexDbContextFactory : IDesignTimeDbContextFactory<TourFlexDbContext>
{
    public TourFlexDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TourFlexDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost\\SQLEXPRESS;Database=SwiftPay;Persist Security Info=true;Integrated Security=true;MultipleActiveResultSets=False;Encrypt=false;TrustServerCertificate=False;Connection Timeout=30;");

        return new TourFlexDbContext(optionsBuilder.Options);
    }
}
