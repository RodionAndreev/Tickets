using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Data.DbModels;

namespace TicketManagement.Data.Repositories
{
    /// <summary>
    ///     Интерфейс для работы с базой
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        ///     Добавляет билет в БД
        /// </summary>
        /// <param name="ticket">Модель билета</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        Task<string> Add(Ticket ticket);

        /// <summary>
        ///     Обновление информации о билете в БД
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
        ///     Удаление всех билетов в БД
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();

        /// <summary>
        ///     Удаление билета с определенным Id в БД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(string id);

        /// <summary>
        ///     Получение всех билет имеющихся в системе в БД
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Ticket>> GetAll();

        /// <summary>
        ///     Получение билета по Id в БД
        /// </summary>
        /// <param name="ticketIdGuid">Id билета</param>
        /// <returns></returns>
        Task<Ticket> Get(string id);

        /// <summary>
        ///     Получение только актуальных билетов в БД
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Ticket>> GetAllActual();

        /// <summary>
        ///     Получение всех билетов имеющихся в системе постранично
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <param name="onlyActual"></param>
        /// <returns></returns>
        Task<Page<Ticket>> GetAllPagebyPage(int index, int pageSize, bool onlyActual = false);
    }
}