using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourFlexSystem.Domain.Models;

namespace TourFlexSystem.Domain.EntityConfiguration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .HasIndex(a => a.AccountNumber)
            .IsUnique();

        builder
            .Property(a => a.AccountNumberExternalReference)
            .HasMaxLength(32)
            .HasColumnType("char");
    }
}
