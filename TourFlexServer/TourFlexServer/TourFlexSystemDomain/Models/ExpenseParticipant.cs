using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TourFlexSystem.Domain.Models;

public class ExpenseParticipant
{
    [ForeignKey(nameof(Expense))]
    public Guid ExpenseId { get; set; }

    public virtual Expense Expense { get; set; } = null!;

    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    public decimal Owes { get; set; }

    public bool HasPaid { get; set; }
}

public class ExpenseParticipantConfiguration : IEntityTypeConfiguration<ExpenseParticipant>
{
    public void Configure(EntityTypeBuilder<ExpenseParticipant> builder)
    {
        builder.HasKey(ep => new { ep.ExpenseId, ep.UserId });
        builder.HasOne(ep => ep.Expense).WithMany(e => e.Participants).HasForeignKey(ep => ep.ExpenseId);
        builder.HasOne(ep => ep.User).WithMany().HasForeignKey(ep => ep.UserId);
    }
}
