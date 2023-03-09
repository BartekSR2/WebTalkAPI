using AutoMapper;
using WebTalkApi.Models;

namespace WebTalkApi.Services
{
    public interface IChatService
    {
        public void Create(AddChatDto chatDto);
        public IEnumerable<BaseChatDto> GetAllChats();

        public void Send(SendMessageDto message, int chatId);

        public ChatDto Chat(int chatId);

    }
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;

        public ChatService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ChatDto Chat(int chatId)
        {
            throw new NotImplementedException();
        }

        public void Create(AddChatDto chatDto)
        {
            throw new NotImplementedException();
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
