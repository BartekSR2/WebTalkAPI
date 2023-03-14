using System.ComponentModel.DataAnnotations;

namespace WebTalkApi.Models
{
    public class SendMessageDto
    {
        [MaxLength(255)]
        [Required]
        public string Content { get; set; }
    }
}
