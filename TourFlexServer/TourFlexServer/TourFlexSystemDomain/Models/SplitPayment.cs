using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TourFlexSystem.Domain.Models;

public class SplitPayment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid ExpenseId { get; set; }

    [ForeignKey(nameof(ExpenseId))]
    public virtual Expense Expense { get; set; } = null!;

    public Guid PayerId { get; set; }

    [ForeignKey(nameof(PayerId))]
    public virtual User Payer { get; set; } = null!;

    public Guid PayeeId { get; set; }

    [ForeignKey(nameof(PayeeId))]
    public virtual User Payee { get; set; } = null!;

    public decimal Amount { get; set; }
    
    public PaymentStatus Status { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string? TransactionId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Settled
}

public class SplitPaymentConfiguration : IEntityTypeConfiguration<SplitPayment>
{
    public void Configure(EntityTypeBuilder<SplitPayment> builder)
    {
        builder.HasOne(sp => sp.Expense).WithMany().HasForeignKey(sp => sp.ExpenseId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(sp => sp.Payer).WithMany().HasForeignKey(sp => sp.PayerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(sp => sp.Payee).WithMany().HasForeignKey(sp => sp.PayeeId).OnDelete(DeleteBehavior.NoAction);
    }
}
