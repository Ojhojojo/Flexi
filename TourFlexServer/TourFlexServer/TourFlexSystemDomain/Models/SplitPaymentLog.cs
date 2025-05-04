using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TourFlexSystem.Domain.Models;

public class SplitPaymentLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid SplitPaymentId { get; set; }

    [ForeignKey(nameof(SplitPaymentId))] 
    public virtual SplitPayment Payment { get; set; } = null!;
    
    [MaxLength(2000)]
    public string LogMessage { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.Now;
}

public class SplitPaymentLogConfiguration : IEntityTypeConfiguration<SplitPaymentLog>
{
    public void Configure(EntityTypeBuilder<SplitPaymentLog> builder)
    {
        builder.HasOne(spl => spl.Payment).WithMany().HasForeignKey(spl => spl.SplitPaymentId).OnDelete(DeleteBehavior.Restrict);
    }
}
