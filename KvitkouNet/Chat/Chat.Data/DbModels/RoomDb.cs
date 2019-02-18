using System;
using System.Collections.Generic;
using System.Text;
using Chat.Data.DbModels.Abstraction;

namespace Chat.Data.DbModels
{
    public class RoomDb : SystemDataForAllModelsDb<string>
    {
        /// <summary>
        /// Id создателя комнаты.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// Название комнаты.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Модификатор доступа комнаты.
        /// </summary>
        public bool IsPrivat { get; set; }

        /// <summary>
        /// пароль для приватной комнаты.
        /// </summary>
        public string Password { get; set; }
    }

}
