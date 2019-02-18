using AutoMapper;
using Chat.Data.DbModels;
using Chat.Logic.Models;

namespace Chat.Logic.MappingProfiles
{
    public class SettingsProfile : Profile
    {
        public SettingsProfile()
        {
            CreateMap<Settings, SettingsDb>().ReverseMap();
        }
    }
}
