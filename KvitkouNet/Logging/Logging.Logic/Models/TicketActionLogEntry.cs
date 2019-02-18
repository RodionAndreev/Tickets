using Logging.Logic.Enums;
using Logging.Logic.Models.Abstraction;

namespace Logging.Logic.Models
{
	/// <summary>
	/// Модель записи в лог о действии с билетом
	/// </summary>
	public class TicketActionLogEntry : BaseLogEntry
	{
		/// <summary>
		/// Id пользователя, выполнившего действие с билетом
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// Id билета
		/// </summary>
		public string TicketId { get; set; }

	    /// <summary>
	    /// Название билета
	    /// </summary>
	    public string TicketName { get; set; }

        /// <summary>
        /// Тип действия с билетом
        /// </summary>
        public TicketActionType ActionType { get; set; }

		/// <summary>
		/// Описание и дополнительное содержимое действия
		/// </summary>
		public string Description { get; set; }
	}
}
