using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Dashboard.Data.DbModels;
using Dashboard.Data.Repositories;
using Dashboard.Logic.Models;
using Dashboard.Logic.Models.Enums;



namespace Dashboard.Logic.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _context;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public DashboardService(IDashboardRepository context, IMapper mapper, IValidator<News> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        ///     Добавляет новость
        /// </summary>
        /// <param name="news">Модель новости</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        public async Task<(string, RequestStatus)> Add(News news)
        {
           if (!_validator.Validate(news).IsValid) return ("0", RequestStatus.BadRequest);
            var res = await _context.Add(_mapper.Map<NewsDb>(news));
            return (res, RequestStatus.Success);
        }


        /// <summary>
        ///     Получение всех новостей имеющихся в системе
        /// </summary>
        /// <returns></returns>
        public async Task<(IEnumerable<News>, RequestStatus)> GetAll()
        {
            var res = _mapper.Map<IEnumerable<News>>(await _context.GetAll());

            var result = res == null ? (null, RequestStatus.Error) : (res, RequestStatus.Success);

            return result;
        }


        /// <summary>
        ///     Удаление всех новостей
        /// </summary>
        /// <returns></returns>
        public async Task<RequestStatus> DeleteAll()
        {
            await _context.DeleteAll();
            return RequestStatus.Success;
        }

        /// <summary>
        ///     Удаление новости с определенным Id
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<RequestStatus> Delete(string newsId)
        {
            await _context.Delete(newsId);
            return RequestStatus.Success;
        }

        /// <summary>
        ///     Получение новости по Id
        /// </summary>
        /// <param name="ticketIdGuid">Id билета</param>
        /// <returns></returns>
        public async Task<(News, RequestStatus)> Get(string newsId)
        {
            var res = await _context.Get(newsId);
            return res == null ? (null, RequestStatus.BadRequest) : (_mapper.Map<News>(res), RequestStatus.Success);
        }

        /// <summary>
        ///     Автоматическое добавление новости
        /// </summary>
        /// <param name="news">Модель новости</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        public async Task<RequestStatus> AddAutoNews(TicketInfo message)
        {
            var news = new News();

            news.NewsStatus = NewsStatus.Hot;
            news.Ticket = message;
            
            if (!_validator.Validate(news).IsValid) return (RequestStatus.BadRequest);
            var res = await _context.Add(_mapper.Map<NewsDb>(news));
            return (RequestStatus.Success);
        }

        #region IDisposable Support

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
