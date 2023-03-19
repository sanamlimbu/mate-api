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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}





