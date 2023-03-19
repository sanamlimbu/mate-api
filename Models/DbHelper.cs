using System;
using OzMateApi.Entities;

namespace OzMateApi.Models
{
	public class DbHelper
	{
		private readonly OzMateContext _context;
		public DbHelper(OzMateContext context)
		{
			_context = context;
		}

		// CRUD Posts
		public List<PostModel> GetPosts()
		{
			List<PostModel> resp = new List<PostModel>();
			var dataList = _context.Posts.ToList();

			dataList.ForEach(row => resp.Add(
				new PostModel()
				{
					Id = row.Id.ToString(),
					Content = row.Content,
					CreatedAt = row.CreatedAt,
					UpdatedAt = row.UpdatedAt,
					DeletedAt = row.DeletedAt,
				}
				));

			return resp;
		}

		public PostModel? GetPostById(string id)
		{
			var guid = new Guid(id);
			PostModel post = new PostModel();
			var data = _context.Posts.Where(d => d.Id.Equals(guid)).FirstOrDefault();

			if (data != null)
			{
				post.Id = data.Id.ToString();
				post.Content = data.Content;
				post.CreatedAt = data.CreatedAt;
				post.UpdatedAt = data.UpdatedAt;
				post.DeletedAt = data.DeletedAt;

                return post;
            }
			return null;
        }

		public void CreatePost(PostModel model)
		{
			Post post = new Post();

			post.Content = model.Content;

			_context.Posts.Add(post);
			_context.SaveChanges();
		}

		public void UpdatePost(string id, PostModel model)
		{
			var guid = new Guid(id);
			var post = _context.Posts.Where(p => p.Id.Equals(guid)).FirstOrDefault();

			if (post != null)
			{
				post.Content = model.Content;
				post.CreatedAt = model.CreatedAt;
				post.DeletedAt = model.UpdatedAt;
				post.DeletedAt = model.DeletedAt;

				_context.SaveChanges();
			}
		}

		public void DeletePost(string id)
		{
            var guid = new Guid(id);
			var post = _context.Posts.Where(p => p.Id.Equals(guid)).FirstOrDefault();
			if (post != null)
			{
				_context.Posts.Remove(post);
				_context.SaveChanges();
			}
		}

        // CRUD Comments
        public List<CommentModel> GetComments()
        {
            List<CommentModel> comments = new List<CommentModel>();
            var dataList = _context.Comments.ToList();

            dataList.ForEach(row => comments.Add(
                new CommentModel()
                {
                    Id = row.Id.ToString(),
                    Content = row.Content,
                    UserId = row.UserId.ToString(),
                    PostId = row.PostId.ToString(),
                    CreatedAt = row.CreatedAt,
                    UpdatedAt = row.UpdatedAt,
                    DeletedAt = row.DeletedAt,
                }
                ));

            return comments;
        }

        public CommentModel? GetCommentById(string id)
        {
            var guid = new Guid(id);
            CommentModel comment = new CommentModel();
            var data = _context.Comments.Where(d => d.Id.Equals(guid)).FirstOrDefault();

            if (data != null)
            {
                comment.Id = data.Id.ToString();
                comment.Content = data.Content;
                comment.UserId = data.UserId.ToString();
                comment.PostId = data.PostId.ToString();
                comment.CreatedAt = data.CreatedAt;
                comment.UpdatedAt = data.UpdatedAt;
                comment.DeletedAt = data.DeletedAt;

                return comment;
            }
            return null;
        }

        public void CreateComment(CommentModel model)
        {
            Comment comment = new Comment();

            comment.Content = model.Content;
            comment.UserId = new Guid(model.UserId);
            comment.PostId = new Guid(model.PostId);

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void UpdateComment(string id, CommentModel model)
        {
            var guid = new Guid(id);
            var comment = _context.Comments.Where(c => c.Id.Equals(guid)).FirstOrDefault();

            if (comment != null)
            {
                comment.Content = model.Content;
                comment.CreatedAt = model.CreatedAt;
                comment.UpdatedAt = model.UpdatedAt;
                comment.DeletedAt = model.DeletedAt;

                _context.SaveChanges();
            }
        }

        public void DeleteComment(string id)
        {
            var guid = new Guid(id);
            var comment = _context.Comments.Where(c => c.Id.Equals(guid)).FirstOrDefault();

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        // CRUD Users
        public void UpdateUser(string id, UserModel model)
        {
            var guid = new Guid(id);
            var user = _context.Users.Where(u => u.Id.Equals(guid)).FirstOrDefault();

            if (user != null)
            {
                user.Name = model.Name;
                user.Gender = model.Gender;
                user.FacebookId = model.FacebookId;
                user.GoogleId = model.GoogleId;
                user.CreatedAt = model.CreatedAt;
                user.UpdatedAt = model.UpdatedAt;
                user.DeletedAt = model.DeletedAt;

                _context.SaveChanges();
            }
        }

        public void DeleteUser(string id)
        {
            var guid = new Guid(id);
            var user = _context.Users.Where(u => u.Id.Equals(guid)).FirstOrDefault();

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}

