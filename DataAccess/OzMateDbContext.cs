using Microsoft.EntityFrameworkCore;
using OzMateApi.Models;
using OzMateApi.Seed;

namespace OzMateApi.DataAccess
{
    public class OzMateDbContext : DbContext
    {
        public OzMateDbContext(DbContextOptions<OzMateDbContext> options)
            : base(options)
        {
            // DateTime error with .NET 6 and PostgreSQL
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add PostgresSQL extension for UUID generation
            modelBuilder.HasPostgresExtension("uuid-ossp");
            //modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ReplyConfiguration());

            // Seed data
            List<User> users = SeedData.GetUsers();
            List<Post> posts = SeedData.GetPosts(users);
            List<Comment> comments = SeedData.GetComments(posts, users);
            List<Reply> replies = SeedData.GetReplies(comments, users);

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Post>().HasData(posts);
            modelBuilder.Entity<Comment>().HasData(comments);
            modelBuilder.Entity<Reply>().HasData(replies);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Media> Media { get; set; }
    }
}

