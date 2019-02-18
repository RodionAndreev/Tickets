using AutoMapper;
using KvitkouNet.Messages.Notification;
using Notification.Logic.Models.Requests;

namespace Notification.Web.MappingProfiles
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<SubscriptionRequest, UserSubscriptionMessage>().ReverseMap();

            CreateMap<UnsubscriptionRequest, UserUnsubscriptionMessage>().ReverseMap();
        }
    }
}