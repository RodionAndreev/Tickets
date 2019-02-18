using AutoMapper;
using Dashboard.Data.DbModels;
using Dashboard.Logic.Models;

namespace Dashboard.Logic.MappingProfiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDb>().ReverseMap();
        }
    }
}
