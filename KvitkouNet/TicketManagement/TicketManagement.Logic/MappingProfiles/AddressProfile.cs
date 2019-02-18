using AutoMapper;
using TicketManagement.Data.DbModels;
using TicketManagement.Logic.Models;

namespace TicketManagement.Logic.MappingProfiles
{
    /// <summary>
    ///     Настройка маппинга для адресов
    /// </summary>
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, SellerAddress>().ReverseMap();
            CreateMap<Address, LocationAddress>().ReverseMap();
        }
    }
}