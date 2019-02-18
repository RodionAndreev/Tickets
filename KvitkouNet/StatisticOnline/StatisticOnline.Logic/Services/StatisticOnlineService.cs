using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StatisticOnline.Data.Context;
using StatisticOnline.Data.Models;
using StatisticOnline.Logic.Interfaces;
using StatisticOnline.Logic.Models;

namespace StatisticOnline.Logic.Services
{
    class StatisticOnlineService: IStatisticOnlineService
    {
        private WebApiContext _context;
        private OnlineDb _dbItem;
        private IMapper _mapper;

        public StatisticOnlineService(WebApiContext context, IMapper mapper)
        {
            _context = context;
            _dbItem = _context.StatisticOnline.OrderBy(p => p.CreateTime).Take(1).FirstOrDefault();
            _mapper = mapper;
        }

        public async Task<OnlineModel> GetAllUsers()
        {
            var item = _context.StatisticOnline.OrderBy(p => p.CreateTime).Take(1).FirstOrDefault();
           return _mapper.Map<OnlineModel>(item);
        }

        public async Task<IEnumerable<OnlineModel>> GetDateRangeUsers(DateRange range)
        {
            var result = _context.StatisticOnline.OrderBy(p => p.CreateTime)
                .Where(db => db.CreateTime >= range.StartDate && db.CreateTime <= range.EndDate)
                .ToList();

            return _mapper.Map<IEnumerable<OnlineModel>>(result);
        }

        public async Task<int> GetRegisteredUser()
        {
            return _dbItem.CountRegistered;
        }

        public async Task<int> GetGuestUser()
        {
            return _dbItem.CountGuest;
        }
    }
}
