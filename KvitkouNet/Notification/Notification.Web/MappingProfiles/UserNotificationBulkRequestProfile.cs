using AutoMapper;
using Notification.Logic.Models;
using Notification.Logic.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Web.MappingProfiles
{
    public class UserNotificationBulkRequestProfile : Profile
    {
        public UserNotificationBulkRequestProfile()
        {
            CreateMap<UserNotificationBulkRequest, NotificationMessage>().ReverseMap();
        }
    }
}
