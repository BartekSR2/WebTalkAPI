using AutoMapper;
using WebTalkApi.Entities;
using WebTalkApi.Models;

namespace WebTalkApi
{
    public class ChatMappingProfiles:Profile
    {
        public ChatMappingProfiles()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(dto => dto.UserName, m => m.MapFrom(m => m.User.Name));
            CreateMap<Chat, ChatDto>();
        }
    }
}
