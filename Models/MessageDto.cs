using System.ComponentModel.DataAnnotations;

namespace WebTalkApi.Models
{
    public class MessageDto
    {
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime SendDate { get; set; }
    }
}
