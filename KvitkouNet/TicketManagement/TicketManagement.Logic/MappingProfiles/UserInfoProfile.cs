using AutoMapper;
using KvitkouNet.Messages.UserManagement;
using KvitkouNet.Messages.UserSettings;
using TicketManagement.Logic.Models;

namespace TicketManagement.Logic.MappingProfiles
{
    /// <summary>
    ///     Настройка маппинга для юзеров
    /// </summary>
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<UserInfo, Data.DbModels.UserInfo>()
                .ReverseMap();
            CreateMap<UserDeletedMessage, Data.DbModels.UserInfo>()
                .ForPath(message => message.UserInfoId,
                    expression => expression.MapFrom(info => info.UserId))
                .ReverseMap();
            CreateMap<DeleteUserProfileMessage, Data.DbModels.UserInfo>()
                .ForPath(message => message.UserInfoId,
                    expression => expression.MapFrom(info => info.UserId))
                .ReverseMap();
            CreateMap<UserUpdatedMessage, Data.DbModels.UserInfo>()
                .ForPath(message => message.UserInfoId,
                    expression => expression.MapFrom(info => info.UserId))
                .ReverseMap();
            CreateMap<UserProfileUpdateMessage, Data.DbModels.UserInfo>()
                .ForPath(message => message.UserInfoId,
                    expression => expression.MapFrom(info => info.UserId))
                .ReverseMap();
        }
    }
}