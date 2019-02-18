using UserManagement.Data.DbModels;
using UserManagement.Logic.Models;

namespace UserManagement.Logic.MappingProfiles
{
    public class UserRegisterProfile : AutoMapper.Profile
    {
        public UserRegisterProfile()
        {
            CreateMap<UserRegisterModel, UserDB>()
                .ForMember(c => c.AccountDB,
                map => map.MapFrom(
                    a => new AccountDB
                    {
                        Login = a.UserName,
                        Email = a.Email,
                        Password = a.Password.GetHashCode().ToString()
                    }))
                .ForMember(c => c.ProfileDB,
                map => map.MapFrom(
                    a => new ProfileDB
                    {
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }))
                .ReverseMap();

        }
    }
}
