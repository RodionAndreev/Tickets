using AutoMapper;
using Chat.Data.DbModels;
using Chat.Logic.Models;

namespace Chat.Logic.MappingProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDb>().ReverseMap();
        }
    }
}
