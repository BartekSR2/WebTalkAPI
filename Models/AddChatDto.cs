using System.ComponentModel.DataAnnotations;

namespace WebTalkApi.Models
{
    public class AddChatDto
    {
        [Required]
        public string Name { get; set; }
    }
}
