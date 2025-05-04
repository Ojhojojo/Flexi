using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TourFlexSystem.Domain.Models;

public class Expense 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }
    
    public decimal Amount { get; set; }

    public Guid CreatedById { get; set; }

    [ForeignKey(nameof(CreatedById))]
    public virtual User CreatedByUser { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<ExpenseParticipant> Participants { get; set; } = new List<ExpenseParticipant>();
    public virtual ICollection<SplitPayment> SplitPayments { get; set; } = new List<SplitPayment>();
}

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasOne(e => e.CreatedByUser).WithMany().HasForeignKey(e => e.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.Participants).WithOne(ep => ep.Expense).HasForeignKey(ep => ep.ExpenseId);

    }
}
