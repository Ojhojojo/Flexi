using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TourFlexSystem.Domain.Models;

public class Account
{
    /// <summary>
    /// Regex to split an unformatted (16 digit without hyphens) account number into groups for formatting
    /// </summary>
    private static readonly Regex UnformattedAccountSplitRegex = new(@"^(\d{2})(\d{4})(\d{7})(\d{3})$");

    /// <summary>
    /// Regex to test whether an account is a valid formatted number.<br/>
    /// Accepts either 16 digits without hyphens or in groups 2-4-7-3.
    /// </summary>
    private static readonly Regex FormattedAccountTestRegex = new(@"^(\d{2})-?(\d{4})-?(\d{7})-?(\d{3})$");

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Account Number (without hyphens)<br/>
    /// e.g. <c>"0112341234567123"</c><br/>
    /// See <see cref="FormattedAccountNumber"/> for helper to access this property as a formatted account number (with hyphens).
    /// </summary>
    [Required]
    [StringLength(16)]
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Helper property <see cref="AccountNumber"/> as a formatted account number<br/>
    /// e.g. <c>"01-1234-1234567-123"</c><br/>
    /// Note that unformatted account numbers may be passed to this method
    /// </summary>
    /// <exception cref="FormatException">When setting the account number if the format is invalid</exception>
    /// <exception cref="InvalidOperationException">If attempting to modify an existing account number</exception>
    [NotMapped]
    public string FormattedAccountNumber
    {
        get => UnformattedAccountSplitRegex.Replace(AccountNumber, "$1-$2-$3-$4");
        init
        {
            if (!FormattedAccountTestRegex.IsMatch(value))
            {
                throw new FormatException("Invalid formatted account number. Expected: '01-1234-1234567-123'");
            }

            AccountNumber = value.Replace("-", "");
        }
    }

    /// <summary>
    /// Image Identifier. This is a string which matches an actual image on the user's device.<br />
    /// 20 Character max.
    /// </summary>
    [MaxLength(20)]
    public string? ImageIdentifier { get; set; }

    /// <summary>
    /// AccountNumberExternalReference will be used to save hash value of Account number.<br />
    /// This will then send to Salesforce.
    /// </summary>
    [MaxLength(32)]
    public string? AccountNumberExternalReference { get; set; }
}

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
