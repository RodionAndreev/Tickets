using UserManagement.Data.DbModels;
using UserManagement.Data.DbModels.Enums;
using UserManagement.Logic.Models;

namespace UserManagement.Logic.MappingProfiles
{
    public class ForViewModelProfile : AutoMapper.Profile
    {
        public ForViewModelProfile()
        {
            CreateMap<ForViewModel, UserDB>()
                .ForPath(x => x.AccountDB.Login,
                    map => map.MapFrom(u => u.Login))
                .ForPath(x => x.ProfileDB.FirstName,
                    map => map.MapFrom(u => u.FirstName))
                .ForPath(x => x.ProfileDB.LastName,
                    map => map.MapFrom(u => u.LastName))
                .ForPath(x => x.ProfileDB.Sex,
                    map => map.MapFrom(u => u.Sex))
                .ForPath(x => x.ProfileDB.Birthday,
                    map => map.MapFrom(u => u.Birthday))
                .ForPath(x => x.ProfileDB.Rating,
                    map => map.MapFrom(u => u.Rating))
                .ForPath(x => x.ProfileDB.RegistrationDate,
                    map => map.MapFrom(u => u.RegistrationDate))
                .ForPath(x => x.AccountDB.Email,
                    map => map.MapFrom(u => u.Email))
                .ForPath(x => x.PhoneNumber,
                    map => map.MapFrom(u => u.PhoneNumber))
                .ForPath(x => x.EmailConfirmed,
                    map => map.MapFrom(u => u.EmailConfirmed))
                .ReverseMap()
                .ForPath(y=>y.Login, 
                    map=>map.MapFrom(u=>u.AccountDB.Login))
                .ForPath(y => y.FirstName,
                    map => map.MapFrom(u => u.ProfileDB.FirstName))
                .ForPath(y => y.LastName,
                    map => map.MapFrom(u => u.ProfileDB.LastName))
                .ForPath(y => y.Sex,
                    map => map.MapFrom(u => u.ProfileDB.Sex))
                .ForPath(y => y.Birthday,
                    map => map.MapFrom(u => u.ProfileDB.Birthday))
                .ForPath(y => y.Rating,
                    map => map.MapFrom(u => u.ProfileDB.Rating))
                .ForPath(y => y.RegistrationDate,
                    map => map.MapFrom(u => u.ProfileDB.RegistrationDate))
                .ForPath(y => y.Email,
                    map => map.MapFrom(u => u.AccountDB.Email))
                .ForPath(y => y.PhoneNumber,
                    map => map.MapFrom(u => u.PhoneNumber));


            
        }
    }
}
