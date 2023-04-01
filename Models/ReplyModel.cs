using System;
using Microsoft.EntityFrameworkCore;
using OzMateApi.Entities;
using OzMateApi.Models;

namespace OzMateApi.Models
{
    public class ReplyModel
    {
        public string Id { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string UserId { get; set; }
        public string CommentId { get; set; }
        public UserModel? User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class ReplyService : DbService
    {
        public ReplyService(OzMateContext context)
            : base(context)
        {
        }

        // CRUD Replies
        public List<ReplyModel> GetReplies()
        {
            List<ReplyModel> resp = new List<ReplyModel>();
            var dataList = _context.Replies.ToList();
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

        public ReplyModel? GetReplyById(string id)
        {
            var guid = new Guid(id);
            ReplyModel reply = new ReplyModel();
            var data = _context.Replies.Where(d => d.Id.Equals(guid)).FirstOrDefault();

            if (data != null)
            {
                reply.Id = data.Id.ToString();
                reply.Content = data.Content;
                reply.UserId = data.UserId.ToString();
                reply.CommentId = data.CommentId.ToString();
                reply.CreatedAt = data.CreatedAt;
                reply.UpdatedAt = data.UpdatedAt;
                reply.DeletedAt = data.DeletedAt;

                return reply;
            }
            return null;
        }

        public void CreateReply(ReplyModel model)
        {
            Reply reply = new Reply();

            reply.Content = model.Content;
            reply.UserId = new Guid(model.UserId);
            reply.CommentId = new Guid(model.CommentId);

            _context.Replies.Add(reply);
            _context.SaveChanges();
        }


        public void UpdateReply(string id, ReplyModel model)
        {
            var guid = new Guid(id);
            var reply = _context.Replies.Where(r => r.Id.Equals(guid)).FirstOrDefault();

            if (reply != null)
            {
                reply.Content = model.Content;
                reply.CreatedAt = model.CreatedAt;
                reply.UpdatedAt = model.UpdatedAt;
                reply.DeletedAt = model.DeletedAt;

                _context.SaveChanges();
            }
        }

        public void DeleteReply(string id)
        {
            var guid = new Guid(id);
            var reply = _context.Replies.Where(r => r.Id.Equals(guid)).FirstOrDefault();

            if (reply != null)
            {
                _context.Replies.Remove(reply);
                _context.SaveChanges();
            }
        }
    }
}





