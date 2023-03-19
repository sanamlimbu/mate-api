using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OzMateApi.Entities
{
    [Table("Users")]
    public class User
    {
        [Key, Required]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
        public string Gender { get; set; }
        public byte[] Image { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .HasIndex(u => u.Id)
               .IsUnique();

            builder
                .Property(u => u.Id)
                .HasColumnName("Id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();

            builder
                .Property(u => u.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()")
                .IsRequired();

            builder
                .Property(u => u.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()")
                .IsRequired();
        }
    }
}

