using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Logic.Models;

namespace TicketManagement.Logic.Services
{
    /// <summary>
    ///     Сервис для работы с Tickets
    /// </summary>
    public interface ITicketService : IDisposable
    {
        /// <summary>
        ///     Добавляет билет
        /// </summary>
        /// <param name="ticket">Модель билета</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        Task<string> Add(Ticket ticket);

        /// <summary>
        ///     Обновление информации о билете
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ticket">Модель билета</param>
        /// <returns></returns>
        Task Update(string id, Ticket ticket);

        /// <summary>
        ///     Добавление пользователя в "я пойду"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ticket">Модель билета</param>
        /// <returns></returns>
        Task AddRespondedUsers(string id, UserInfo user);

        /// <summary>
        ///     Удаление всех билетов
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();

        /// <summary>
        ///     Удаление билета с определенным Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(string id);

        /// <summary>
        ///     Получение всех билет имеющихся в системе
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Ticket>> GetAll();

        /// <summary>
        ///     Получение билета по Id
        /// </summary>
        /// <returns></returns>
        Task<Ticket> Get(string id);

        /// <summary>
        ///     Получение только актуальных билетов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Ticket>> GetAllActual();

        /// <summary>
        ///     Получение всех билетов имеющихся в системе постранично
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<Page<TicketLite>> GetAllPagebyPage(int index);

        /// <summary>
        ///     Получение всех актуальных билетов имеющихся в системе постранично
        /// </summary>
        /// <param name="index"></param>
        /// <param name="onlyActual">Только актуальные билеты</param>
        /// <returns></returns>
        Task<Page<TicketLite>> GetAllPagebyPageActual(int index, bool onlyActual = true);
    }
}