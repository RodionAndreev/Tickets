using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KvitkouNet;
using Notification.Logic.Models.Enums;

namespace Notification.Web.MappingProfiles
{
    public class SeverityProfile : Profile
    {
        public SeverityProfile()
        {
            CreateMap<KvitkouNet.Messages.Notification.Enums.Severity, Logic.Models.Enums.Severity>().ReverseMap();
        }
    }
}
