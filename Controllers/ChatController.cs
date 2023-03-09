using Microsoft.AspNetCore.Mvc;
using WebTalkApi.Models;
using WebTalkApi.Services;

namespace WebTalkApi.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public ActionResult CreateChat([FromBody] AddChatDto chatDto)
        {

            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<BaseChatDto>> ShowAllChats()
        {

            return Ok(null);
        }

        [HttpPost("{chatId}/message")]
        public ActionResult SendMessage([FromBody] SendMessageDto messageDto, [FromRoute] int chatId)
        {

            return Ok();
        }

        [HttpGet("{chatId}/message")]
        public ActionResult<ChatDto> ShowChat([FromRoute] int chatId)
        {

            return null;
        }


    }
}
