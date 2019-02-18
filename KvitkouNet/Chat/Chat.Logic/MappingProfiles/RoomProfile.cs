using AutoMapper;
using Chat.Data.DbModels;
using Chat.Logic.Models;

namespace Chat.Logic.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDb>().ReverseMap();
        }
    }
}
