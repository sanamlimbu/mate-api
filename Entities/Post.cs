using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OzMateApi.Entities
{
    [Table("Posts")]
    public class Post
    {
        [Key, Required]
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
               .HasIndex(p => p.Id)
               .IsUnique();

            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();

            builder
               .HasOne(p => p.User)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.UserId);

            builder
                .Property(p => p.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()")
                .IsRequired();

            builder
                .Property(p => p.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()")
                .IsRequired();
        }
    }
}

