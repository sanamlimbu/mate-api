using Bogus;
using OzMateApi.Models;

namespace OzMateApi.Seed
{
	public static class SeedData
	{

		static readonly int count = 10;
        static readonly Faker faker = new Faker();


        // Users
        public static List<User> GetUsers()
		{
			List<User> users = new List<User>();
			for (int i=0; i < count; i++)
			{
                User user = new User
                {
                    Id = Guid.NewGuid(),
                    FirebaseUid = Guid.NewGuid().ToString(),
                    DisplayName = faker.Name.FullName(),
                    EmailVerified = true,
                    Email = faker.Internet.Email(),
				};
				users.Add(user);
			}

			return users;
		}

		// Posts
		public static List<Post> GetPosts(List<User> users)
		{
			List<Post> posts = new List<Post>();

			foreach (User user in users)
			{
                for (int i = 0; i < 5; i++)
                {
                    Post post = new Post
                    {
                        Id = Guid.NewGuid(),
                        Content = faker.Lorem.Paragraph(2),
                        UserId = user.Id,
                    };
                    posts.Add(post);
                }
            }
            return posts;

        }

        // Comments
        public static List<Comment> GetComments(List<Post> posts, List<User> users)
        {
            List<Comment> comments = new List<Comment>();

            foreach (Post post in posts)
            {
                List<User> fourUniqueUsers = users.OrderBy(u => Guid.NewGuid()).Take(4).ToList();
                foreach (User user in fourUniqueUsers)
                {
                    Comment comment = new Comment
                    {
                        Id = Guid.NewGuid(),
                        Content = faker.Lorem.Paragraph(1),
                        UserId = user.Id,
                        PostId = post.Id,
                    };
                    comments.Add(comment);
                }
            }
            return comments;
        }

        // Replies
        public static List<Reply> GetReplies(List<Comment> comments, List<User> users)
        {
            List<Reply> replies = new List<Reply>();

            foreach (Comment comment in comments)
            {
                List<User> fourUniqueUsers = users.OrderBy(u => Guid.NewGuid()).Take(4).ToList();
                foreach (User user in fourUniqueUsers)
                {
                    Reply reply = new Reply
                    {
                        Id = Guid.NewGuid(),
                        Content = faker.Lorem.Paragraph(1),
                        UserId = user.Id,
                        CommentId = comment.Id,
                    };
                    replies.Add(reply);
                }
            }
            return replies;
        }
    }
}

