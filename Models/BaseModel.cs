using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OzMateApi.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }
}

