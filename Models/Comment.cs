using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OzMateApi.Models
{
    public class Comment : BaseModel
    {
        public string? Content { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public Guid? MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }
    }

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            // Apply common configuration
            CommonEntityConfiguration.Configure(builder);

            builder
              .HasOne(e => e.User)
              .WithMany(e => e.Comments)
              .HasForeignKey(e => e.UserId)
              .IsRequired();

            builder
                .HasOne(e => e.Post)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.PostId)
                .IsRequired();
            builder
                .HasOne(e => e.Media);
        }
    }
}
