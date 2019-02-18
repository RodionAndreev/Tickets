using Logging.Data.DbModels.Abstraction;

namespace Logging.Data.DbModels
{
    /// <summary>
    /// Модель для логирования поисковых запросов пользователей.
    /// <para>Текст запроса хранится в свойстве Content класса BaseLogEntry.</para>
    /// </summary>
    public class SearchQueryLogEntryDbModel : BaseLogEntryDbModel
    {
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
