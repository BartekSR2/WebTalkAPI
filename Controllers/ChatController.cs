using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTalkApi.Models;
using WebTalkApi.Services;

namespace WebTalkApi.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public ActionResult CreateNewChat([FromBody] AddChatDto chatDto)
        {
            _chatService.CreateChat(chatDto);
            return NoContent();
        }
        [HttpPost("{chatId}/user/{userId}")]
        public ActionResult AddNewUserToChat([FromRoute]int chatId, [FromRoute] int userId)
        {
            _chatService.AddUser(chatId, userId);

            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<BaseChatDto>> ShowAccesibleChats()
        {
            var result = _chatService.GetAllChats();
            return Ok(result);
        }

        [HttpPost("{chatId}/message")]
        public ActionResult SendMessage([FromBody] SendMessageDto messageDto, [FromRoute] int chatId)
        {
            _chatService.Send(messageDto, chatId);
            return Ok();
        }

        [HttpGet("{chatId}/message")]
        public ActionResult<ChatDto> ShowChat([FromRoute] int chatId, [FromQuery]int amountOfMessages )
        {
            var result = _chatService.GetChat(chatId, amountOfMessages);
            return Ok(result);
        }


    }
}
