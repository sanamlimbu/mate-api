using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OzMateApi.Models
{
    public class Reply : BaseModel
    {
        public string? Content { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; }
        public Guid? MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }

    }

    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            // Apply common configuration
            CommonEntityConfiguration.Configure(builder);

            builder
                .HasOne(e => e.Comment)
                .WithMany(e => e.Replies)
                .HasForeignKey(r => r.CommentId);
        }
    }
}





