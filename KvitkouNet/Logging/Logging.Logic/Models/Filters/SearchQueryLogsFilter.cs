using Logging.Logic.Models.Filters.Abstraction;

namespace Logging.Logic.Models.Filters
{
	public class SearchQueryLogsFilter : BaseLogFilter
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
