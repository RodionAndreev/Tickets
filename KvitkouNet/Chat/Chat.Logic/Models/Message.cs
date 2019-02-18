using System;

namespace Chat.Logic.Models
{
    public class Message
    {
        /// <summary>
        /// Id сообщения.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id пользователя кто отправил сообщение.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Время отправки.
        /// </summary>
        public DateTime SendedTime { get; set; }

        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Флаг показывающий было ли отредактировано сообщение.
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
