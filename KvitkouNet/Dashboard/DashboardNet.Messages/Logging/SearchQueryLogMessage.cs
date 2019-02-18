using System;

namespace KvitkouNet.Messages.Logging
{
    /// <summary>
    /// Модель для сообщении о поисковых запросах пользователей
    /// </summary>
    public class SearchQueryLogMessage
    {
        /// <summary>
        /// Дата логируемого события
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Id пользователя, выполнившего поисковый запрос
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Содержимое строки поиска
        /// </summary>
        public string SearchCriterium { get; set; }

        /// <summary>
        /// Состояние фильтров при выполнении запросов
        /// </summary>
        public string FilterInfo { get; set; }
    }
}