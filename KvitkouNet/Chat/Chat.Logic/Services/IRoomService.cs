using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chat.Logic.Models;

namespace Chat.Logic.Services
{
    public interface IRoomService : IDisposable
    {
        /// <summary>
        /// Создание комнаты.
        /// </summary>
        /// <returns></returns>
        Task AddRoom(Room room, string userId);

        /// <summary>
        /// Получение доступных комнат
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetRooms(string userId);

        /// <summary>
        /// Поиск комнаты по названию
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Room>> SearchRoom(string template);

        /// <summary>
        /// Получение сообщений из комнаты, согласно ограничению по истории
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Message>> GetMessages(string roomId, int historyCountsMessages);

        /// <summary>
        /// Поиск сообщения в комнате
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Message>> SearchMessage(string roomId, string template);

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <returns></returns>
        Task<string> AddMessage(Message message, string roomId);

        /// <summary>
        /// Редактирование сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task EditMessage(Message message, string roomId);

        /// <summary>
        /// Удаление сообщения
        /// </summary>
        /// <returns></returns>
        Task DeleteMessage(string messageId);
    }
}
