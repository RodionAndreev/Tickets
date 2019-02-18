using System.Collections.Generic;

namespace Chat.Logic.Models
{
    public class Chat
    {
        /// <summary>
        /// Пользовательские настройки для чата.
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// Список комнат.
        /// </summary>
        public List<Room> Rooms { get; set; }

    }
}
