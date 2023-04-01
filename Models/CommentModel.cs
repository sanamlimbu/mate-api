using System;
using Microsoft.EntityFrameworkCore;
using OzMateApi.Entities;
using OzMateApi.Models;

namespace OzMateApi.Models
{
    public class CommentModel
    {
        public string Id { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string UserId { get; set; }
        public string PostId { get; set; }
        public UserModel? User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class CommentService: DbService
    {
        public CommentService(OzMateContext context)
            : base(context)
        {
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

        public CommentModel[]? GetCommentsByPostId(string id)
        {
            var guid = new Guid(id);
            var data = _context.Comments.Where(d => d.PostId.Equals(guid)).ToArray();

            if (data != null && data.Length > 0)
            {
                var result = new CommentModel[data.Length];
                for (int i = 0; i < data.Length; i++) {

                    var userService = new UserService(_context);
                    var user = userService.GetUserById(data[i].UserId.ToString());

                    result[i] = new CommentModel
                    {
                        Id = data[i].Id.ToString(),
                        Content = data[i].Content,
                        UserId = data[i].UserId.ToString(),
                        PostId = data[i].PostId.ToString(),
                        CreatedAt = data[i].CreatedAt,
                        UpdatedAt = data[i].UpdatedAt,
                        DeletedAt = data[i].DeletedAt,
                        User = user,
                    };
                }
                return result;
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

        public List<ReplyModel> GetCommentReplies(string commentId)
        {
            List<ReplyModel> resp = new List<ReplyModel>();
            var dataList = _context.Replies.Where(d => d.CommentId.Equals(commentId)).ToList();
            var userService = new UserService(_context);

            dataList.ForEach(row =>
            {
                var user = userService.GetUserById(row.UserId.ToString());
                if (user != null)
                {
                    resp.Add(
                        new ReplyModel()
                        {
                            Id = row.Id.ToString(),
                            Content = row.Content,
                            CreatedAt = row.CreatedAt,
                            UpdatedAt = row.UpdatedAt,
                            DeletedAt = row.DeletedAt,
                            User = user
                        });
                }
            });

            return resp;

        }
    }
}





