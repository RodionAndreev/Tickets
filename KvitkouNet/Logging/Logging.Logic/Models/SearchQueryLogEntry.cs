using Logging.Logic.Models.Abstraction;

namespace Logging.Logic.Models
{
	/// <summary>
	/// Модель для логирования поисковых запросов пользователей
	/// </summary>
	public class SearchQueryLogEntry : BaseLogEntry
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
