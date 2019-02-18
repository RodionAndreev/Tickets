using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using StatisticUser.Data;
using StatisticUser.Data.DbModels;
using StatisticUser.Logic.DTOs;
using StatisticUser.Logic.Interfaces;

namespace StatisticUser.Logic.Services
{
    public class StatisticUserService: IStatisticUserService
    {
        private WebApiContext _service;

        public StatisticUserService(WebApiContext service)
        {
            _service = service;
        }

        public void Dispose()
        {

        }

        public Task<IEnumerable<ITimeOnResouces>> GetTimeOnResouces(DateRange filter)
        {
            throw new System.NotImplementedException();
        }

        public Task<IUserOnline> GetUserOnline(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IUserRating> GetUserRating(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IRegistrationTime>> GetRegistrationsTime(DateRange filter)
        {
            throw new System.NotImplementedException();
        }

        public Task<IUserMessages> GetUserMessages(int id)
        {
            throw new System.NotImplementedException();
        }

        public LoadResult GetAllUser(DataSourceLoadOptions loadOptions)
        {
            var result= DataSourceLoader.Load(
                _service.SummaryTable,
                loadOptions);

            return result;
        }
    }
}