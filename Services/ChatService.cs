using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebTalkApi.Entities;
using WebTalkApi.Exceptions;
using WebTalkApi.Models;

namespace WebTalkApi.Services
{
    public interface IChatService
    {
        public void CreateChat(AddChatDto chatDto);
        public void AddUser(int chatId, int userId);
        public IEnumerable<BaseChatDto> GetAllChats();

        public void Send(SendMessageDto message, int chatId);


        public ChatDto GetChat(int chatId, int ammountOfMessages);

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

        public void AddUser(int chatId, int userId)
        {
            var chat = _dbContext
                .Chats
                .Include(c => c.Users)
                .FirstOrDefault(c => c.Id == chatId);

            if(chat is null)
            {
                throw new NotFoundException("Chat not found");
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if(user is null)
            {
                throw new NotFoundException("User not found");
            }

            var userInChat = chat.Users.Any(u => u.Id == userId);

            if (userInChat)
            {
                throw new BadRequestException("User is already added in chat");
            }

            chat.Users.Add(user);
            _dbContext.SaveChanges();

        }

        public ChatDto GetChat(int chatId, int ammountOfMessages)
        {
            if(ammountOfMessages < 0)
            {
                throw new BadRequestException("numer of messages  cant be smaller than 0");
            }
            var chat = _dbContext
                .Chats
                .Include(c => c.Messages.Take(ammountOfMessages))
                .Include(u => u.Users)
                .FirstOrDefault(c => c.Id == chatId);

            if(chat is null)
            {
                throw new NotFoundException("Chat not found");
            }

            var userInChat = chat.Users.Any(u => u.Id == _userContext.UserId);
            if (!userInChat)
            {
                throw new ForbidException("No chat access");
            }

            var messagesDto = _mapper.Map<List<MessageDto>>(chat.Messages);

            var result = new ChatDto()
            {
                Id = chat.Id,
                Name = chat.Name,
                Messages = messagesDto

            };

            return result;

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
            var user = _dbContext
                .Users
                .Include(u => u.Chats)
                .FirstOrDefault(u => u.Id == _userContext.UserId);

            var userChats = user.Chats;

            var result = _mapper.Map<List<BaseChatDto>>(userChats);
            

            return result;


            


        }

        public void Send(SendMessageDto message, int chatId)
        {
            var chat = _dbContext
                .Chats
                .Include(c => c.Users)
                .Include(c => c.Messages)
                .FirstOrDefault(c => c.Id == chatId);
            if(chat is null)
            {
                throw new NotFoundException("Chat not found");
            }
            var userInChat = chat.Users.Any(u => u.Id == _userContext.UserId);

            if (!userInChat)
            {
                throw new ForbidException("No chat access");
            }

            var messageToSend = new Message()
            {
                Content = message.Content,
                SendDate = DateTime.Now,
                UserId = (int)_userContext.UserId,
            };

            chat.Messages.Add(messageToSend);
            _dbContext.SaveChanges();


        }

        
    }
}
