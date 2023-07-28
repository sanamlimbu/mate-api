using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OzMateApi.Models
{
    public class Post : BaseModel
    {
        public string? Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
        public IEnumerable<Media>? Medias { get; set; }
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Apply common configuration
            CommonEntityConfiguration.Configure(builder);

            builder
               .HasOne(e => e.User)
               .WithMany(e => e.Posts)
               .HasForeignKey(e => e.UserId)
               .IsRequired();

            builder
                .HasMany(e => e.Medias)
                .WithOne()
                .HasForeignKey(e => e.Id)
                .IsRequired();
        }
    }
}
