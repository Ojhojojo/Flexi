using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TourFlexSystem.Domain.Models;

public class Friend
{
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;


    public Guid FriendUserId { get; set; }

    [ForeignKey(nameof(FriendUserId))]
    public virtual User FriendUser { get; set; } = null!;

    [Required]
    [MaxLength(15)]
    public FriendStatus Status { get; set; }
}

public enum FriendStatus
{
    Pending,
    Accepted,
    Rejected
}

public class FriendConfiguration : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.HasKey(f => new { f.UserId, f.FriendUserId });
        builder.HasOne(f => f.User).WithMany().HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(f => f.FriendUser).WithMany().HasForeignKey(f => f.FriendUserId).OnDelete(DeleteBehavior.NoAction);

    }
}
