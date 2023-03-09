using System.ComponentModel.DataAnnotations;

namespace WebTalkApi.Models
{
    public class MessageDto
    {
        public int Content { get; set; }
        public string UserName { get; set; }
        public DateTime SendDate { get; set; }
    }
}
