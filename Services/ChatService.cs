using AutoMapper;
using WebTalkApi.Entities;
using WebTalkApi.Models;

namespace WebTalkApi.Services
{
    public interface IChatService
    {
        public void CreateChat(AddChatDto chatDto);
        public IEnumerable<BaseChatDto> GetAllChats();

        public void Send(SendMessageDto message, int chatId);

        public ChatDto Chat(int chatId);

    }
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly WebTalkDbContext _dbContext;

        public ChatService(WebTalkDbContext dbcontext, IMapper mapper, IUserContext userContext)
        {
            _mapper = mapper;
            _userContext = userContext;
            _dbContext = dbcontext;
        }
        public ChatDto Chat(int chatId)
        {
            throw new NotImplementedException();
        }

        public void CreateChat(AddChatDto chatDto)
        {
            var newChat = new Chat()
            {
                Name = chatDto.Name,
                CreationDate = DateTime.Now,
                CreatedByUserId = _userContext.UserId.Value,
                Users = new List<User>(),
                Messages = new List<Message>()
                

            };

            var firstUser = _dbContext.Users.FirstOrDefault(u => u.Id == _userContext.UserId);
            
            newChat.Users.Add(firstUser);

            _dbContext.Chats.Add(newChat);
            _dbContext.SaveChanges();

        }

        public IEnumerable<BaseChatDto> GetAllChats()
        {
            throw new NotImplementedException();
        }

        public void Send(SendMessageDto message, int chatId)
        {
            throw new NotImplementedException();
        }

        
    }
}
