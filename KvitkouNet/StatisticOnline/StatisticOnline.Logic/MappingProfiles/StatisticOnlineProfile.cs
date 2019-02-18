using AutoMapper;
using StatisticOnline.Data.Models;
using StatisticOnline.Logic.Models;

namespace StatisticOnline.Logic.MappingProfiles
{
    class StatisticOnlineProfile: Profile
    {
        public StatisticOnlineProfile()
        {
            CreateMap<OnlineModel, OnlineDb>().ReverseMap();
        }
    }
}
