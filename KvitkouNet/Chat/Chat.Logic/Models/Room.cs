using System.Collections.Generic;

namespace Chat.Logic.Models
{
    public class Room
    {
        /// <summary>
        /// Id комнаты.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название комнаты.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Модификатор доступа комнаты. B
        /// </summary>
        public bool IsPrivat { get; set; }

        /// <summary>
        /// пароль для приватной комнаты.
        /// </summary>
        public string Password { get; set; }
    }
}
