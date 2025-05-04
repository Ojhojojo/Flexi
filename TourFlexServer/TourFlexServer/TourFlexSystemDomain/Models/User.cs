using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TourFlexSystem.Domain.Models;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    // Firebase Id
    public string UserIdentityId { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    // CoreBanking Id
    public string UserAccountId { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string MobileNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(260)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(15)]
    public UserStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public virtual IEnumerable<Account>? Accounts { get; set; }
}


public enum UserStatus
{
    New, // Allows User to use Savvy Split
    Active, // Allows User to use Split and Pay
    Cancelled,
    Deleted
}


public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(b => b.Status)
            .HasDefaultValue(UserStatus.New);

        builder
            .HasIndex(c => c.UserIdentityId)
            .IsUnique();

        builder
            .HasIndex(c => c.Username)
            .IsUnique();

        builder
            .HasIndex(c => c.Email)
            .IsUnique();

        builder
            .HasIndex(c => c.MobileNumber)
            .IsUnique();

        builder
            .HasIndex(c => c.Status);
    }
}
