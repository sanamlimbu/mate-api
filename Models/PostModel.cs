using System;
using OzMateApi.Entities;

namespace OzMateApi.Models
{
	public class PostModel
	{
        public string Id { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public CommentModel[]? Comments { get; set; }
        public UserModel user { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class PostService : DbService
    {
        public PostService(OzMateContext context)
            : base(context)
        {
        }

        // CRUD Posts
        public List<PostModel> GetPosts()
        {
            List<PostModel> resp = new List<PostModel>();
            var dataList = _context.Posts.ToList();

            dataList.ForEach(row => {
                var userService = new UserService(_context);
                var user = userService.GetUserById(row.UserId.ToString());
                if (user != null)
                {
                    resp.Add(
                        new PostModel()
                        {
                            Id = row.Id.ToString(),
                            Content = row.Content,
                            CreatedAt = row.CreatedAt,
                            UpdatedAt = row.UpdatedAt,
                            DeletedAt = row.DeletedAt,
                            user = user
                        });
                }
            });

            return resp;
        }

        public PostModel? GetPostById(string id)
        {
            var guid = new Guid(id);
            PostModel post = new PostModel();
            var data = _context.Posts.Where(d => d.Id.Equals(guid)).FirstOrDefault();

            if (data != null)
            { 
                var commentService = new CommentService(_context);
                var comments = commentService.GetCommentsByPostId(data.Id.ToString());
               
                post.Id = data.Id.ToString();
                post.Content = data.Content;
                post.CreatedAt = data.CreatedAt;
                post.UpdatedAt = data.UpdatedAt;
                post.DeletedAt = data.DeletedAt;
                post.Comments = comments;

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
    }
}

