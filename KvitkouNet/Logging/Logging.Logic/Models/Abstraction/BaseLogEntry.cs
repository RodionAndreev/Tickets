using System;

namespace Logging.Logic.Models.Abstraction
{
	/// <summary>
	/// Базовый класс для всех доменных моделей записей лога
	/// </summary>
	public abstract class BaseLogEntry
	{
		/// <summary>
		/// Id записи
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Дата логируемого события
		/// </summary>
		public DateTime EventDate { get; set; }
	}
}
