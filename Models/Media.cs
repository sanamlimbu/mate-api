using System.ComponentModel.DataAnnotations;

namespace OzMateApi.Models
{
    public class Media : BaseModel
    {
        [Required]
        public string MimeType { get; set; }
        [Required]
        public required long FileSizeBytes { get; set; }
        [Required]
        public required string Extension { get; set; }
        [Required]
        public required string Url { get; set; }


    }
}

