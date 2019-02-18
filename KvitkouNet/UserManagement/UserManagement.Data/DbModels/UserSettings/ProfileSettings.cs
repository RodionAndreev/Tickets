using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Data.DbModels.UserSettings
{
    /// <summary>
    /// Класс настроек пользователя
    /// </summary>
    public class ProfileSettings
    {
        /// <summary>
        /// Уникальный идентификатор настроек
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Аватарка пользователся
        /// </summary>
        public byte[] UserImage { get; set; }

        /// <summary>
        /// Логин пользователя. Доступен только для просмотра.
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// Флаг, отвечающий за закрытость аккаунта для гостей.
        /// </summary>
        public bool IsPrivateAccount { get; set; }

        /// <summary>
        /// Предпочитаемый адрес доступных билетов.
        /// </summary>
        public string PreferAddress { get; set; }

        /// <summary>
        /// Предпочитаемый район доступных билетов.
        /// </summary>
        public string PreferRegion { get; set; }

        /// <summary>
        /// Флаг, отвечающий за получение информации о билетах.
        /// </summary>
        public bool IsGetTicketInfo { get; set; }

        /// <summary>
        /// Ссылки на социальные сети.
        /// </summary>
        [NotMapped]
        public IEnumerable<object> SocialNetworks { get; set; }

        //TODO: object - заменить на класс статистики
        /// <summary>
        /// Статистика билетов пользователя.
        /// </summary>
        [NotMapped]
        public object GetTicketStatistic { get; set; }

        /// <summary>
        /// Флаг, отвечающий за то что было выбрано изменение пароля.
        /// </summary>
        public bool IsChangePassword { get; set; }

        /// <summary>
        /// Предпочитаемое место посещения
        /// </summary>
        public string PreferPlace { get; set; }


    }
}
