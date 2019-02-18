using AutoMapper;
using Chat.Data.DbModels;
using Chat.Logic.Models;

namespace Chat.Logic.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDb>().ReverseMap();
        }
    }
}
