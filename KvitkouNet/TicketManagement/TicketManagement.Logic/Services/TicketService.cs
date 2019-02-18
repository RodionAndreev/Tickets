using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using FluentValidation;
using KvitkouNet.Messages.Logging;
using KvitkouNet.Messages.Logging.Enums;
using KvitkouNet.Messages.TicketManagement;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Retry;
using TicketManagement.Data.Repositories;
using TicketManagement.Logic.Exceptions;
using TicketManagement.Logic.Models;

namespace TicketManagement.Logic.Services
{
    /// <summary>
    ///     Сервис для работы с тикетами
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly ITicketRepository _context;
        private readonly IMapper _mapper;
        private readonly RetryPolicy _policy;
        private readonly IValidator _validatorTickets;
        private readonly IValidator<UserInfo> _validatorUsers;

        public TicketService(ITicketRepository context,
            IMapper mapper,
            IValidator<Ticket> validatorTickets,
            IConfiguration configuration,
            IBus bus,
            IValidator<UserInfo> validatorUsers)
        {
            _context = context;
            _mapper = mapper;
            _validatorTickets = validatorTickets;
            _configuration = configuration;
            _bus = bus;
            _validatorUsers = validatorUsers;
            _policy = Policy.Handle<TimeoutException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1)
                });
        }

        /// <summary>
        ///     Добавляет билет
        /// </summary>
        /// <param name="ticket">Модель билета</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        public async Task<string> Add(Ticket ticket)
        {
            var validationResultTicket = await _validatorTickets.ValidateAsync(ticket);
            var validationResultUser = await _validatorUsers.ValidateAsync(ticket.User);
            if (!validationResultTicket.IsValid | !validationResultUser.IsValid)
            {
                var errors = validationResultTicket.Errors.ToList();
                errors.AddRange(validationResultUser.Errors.ToArray());
                throw new ValidationException("Validation failed",
                    errors);
            }

            var res = await _context.Add(_mapper.Map<Data.DbModels.Ticket>(ticket));
            try
            {
                await _policy.ExecuteAsync(async () =>
                {
                    await _bus.PublishAsync(new TicketCreationMessage
                    {
                        TicketId = res,
                        Price = ticket.Price,
                        Name = ticket.Name,
                        City = ticket.LocationEvent.City,
                        Category = ticket.TypeEvent,
                        Date = DateTime.Now
                    });
                    await _bus.PublishAsync(new TicketActionLogMessage
                    {
                        TicketId = res,
                        UserId = ticket.User.UserInfoId,
                        ActionType = TicketAction.Add,
                        EventDate = DateTime.Now
                    });
                });
            }
            catch (TimeoutException exception)
            {
                throw new EasyNetQSendException("Ticket added in db, but error sending message to RabbitMQ",
                    exception,
                    res);
            }

            return res;
        }

        /// <summary>
        ///     Обновление информации о билете
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ticket">Модель билета</param>
        /// <returns></returns>
        public async Task Update(string id,
            Ticket ticket)
        {
            await _context.Update(id,
                _mapper.Map<Data.DbModels.Ticket>(ticket));
            try
            {
                await _policy.ExecuteAsync(async () =>
                {
                    await _bus.PublishAsync(new TicketUpdatedMessage
                    {
                        TicketId = id,
                        Price = ticket.Price,
                        Name = ticket.Name,
                        City = ticket.LocationEvent.City,
                        Category = ticket.TypeEvent,
                        Date = DateTime.Now
                    });
                    await _bus.PublishAsync(new TicketActionLogMessage
                    {
                        TicketId = id,
                        UserId = ticket.User.UserInfoId,
                        ActionType = TicketAction.Update
                    });
                });
            }
            catch (TimeoutException exception)
            {
                throw new EasyNetQSendException("Ticket added in db, but error sending message to RabbitMQ",
                    exception);
            }
        }

        /// <summary>
        ///     Добавление пользователя в "я пойду"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task AddRespondedUsers(string id,
            UserInfo user)
        {
            var validateAct = _validatorUsers.Validate(user);
            if (!validateAct.IsValid)
                throw new ValidationException("Validation failed",
                    validateAct.Errors);
            await _context.AddRespondedUsers(id,
                _mapper.Map<Data.DbModels.UserInfo>(user));
        }

        /// <summary>
        ///     Удаление всех билетов
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAll()
        {
            await _context.DeleteAll();
        }

        /// <summary>
        ///     Удаление билета с определенным Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(string id)
        {
            await _context.Delete(id);
            try
            {
                await _policy.ExecuteAsync(async () =>
                {
                    await _bus.PublishAsync(new TicketDeletedMessage
                    {
                        TicketId = id
                    });
                    await _bus.PublishAsync(new TicketActionLogMessage
                    {
                        TicketId = id,
                        ActionType = TicketAction.Delete
                    });
                });
            }
            catch (TimeoutException exception)
            {
                throw new EasyNetQSendException("Error sending message to RabbitMQ",
                    exception);
            }
        }

        /// <summary>
        ///     Получение всех билет имеющихся в системе
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ticket>> GetAll()
        {
            var res = _mapper.Map<IEnumerable<Ticket>>(await _context.GetAll());
            return res;
        }

        /// <summary>
        ///     Получение билета по Id
        /// </summary>
        /// <returns></returns>
        public async Task<Ticket> Get(string id)
        {
            var res = _mapper.Map<Ticket>(await _context.Get(id));
            try
            {
                await _policy.ExecuteAsync(async () =>
                {
                    await _bus.PublishAsync(new TicketActionLogMessage
                    {
                        TicketId = id,
                        ActionType = TicketAction.Gaze
                    });
                });
            }
            catch (TimeoutException exception)
            {
                throw new EasyNetQSendException("Error sending message to RabbitMQ",
                    exception, res);
            }

            return res;
        }

        /// <summary>
        ///     Получение только актуальных билетов
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ticket>> GetAllActual()
        {
            var res = _mapper.Map<IEnumerable<Ticket>>(await _context.GetAllActual());
            return res;
        }

        /// <summary>
        ///     Получение всех билетов имеющихся в системе постранично
        /// </summary>
        /// <param name="index">Номер текущей страницы</param>
        /// <returns></returns>
        public async Task<Page<TicketLite>> GetAllPagebyPage(int index)
        {
            var pageSize = _configuration.GetValue<int>("pageSize");
            var res = _mapper.Map<Page<TicketLite>>(await _context.GetAllPagebyPage(index,
                pageSize));
            return res;
        }

        /// <summary>
        ///     Получение всех актуальных билетов имеющихся в системе постранично
        /// </summary>
        /// <param name="index">Номер текущей страницы</param>
        /// <param name="onlyActual">Только актуальные билеты</param>
        /// <returns></returns>
        public async Task<Page<TicketLite>> GetAllPagebyPageActual(int index,
            bool onlyActual = true)
        {
            var pageSize = _configuration.GetValue<int>("pageSize");
            var res = _mapper.Map<Page<TicketLite>>(await _context.GetAllPagebyPage(index,
                pageSize,
                onlyActual));
            return res;
        }

        /// <summary>
        ///     Disposing
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}