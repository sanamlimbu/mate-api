using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OzMateApi.Models
{
    public class User : BaseModel
    {
        [Required]
        public string FirebaseUid { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public bool EmailVerified { get; set; }
        public string? FirebasePhotoURL { get; set; }
        public string? ProfileURL { get; set; }
        public string? BannerURL { get; set; }
        [JsonIgnore]
        public ICollection<Post>? Posts { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }
        [JsonIgnore]
        public ICollection<Reply>? Replies { get; set; }

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Apply common configuration
            CommonEntityConfiguration.Configure(builder);

            builder
               .HasIndex(e => e.FirebaseUid)
               .IsUnique();
        }
    }
}


