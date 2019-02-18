using System;

namespace Logging.Logic.Enums
{
	/// <summary>
	/// Перечисление, описывающее действия с билетом
	/// </summary>
	[Flags]
	public enum TicketActionType
	{
		/// <summary>
		/// Неизвестное действие
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Просмотр (подробный) билета
		/// </summary>
		Gaze = 1 << 0,

		/// <summary>
		/// Размещение билета
		/// </summary>
		Add = 1 << 1,

		/// <summary>
		/// Обновление данных билета
		/// </summary>
		Update = 1 << 2,

		/// <summary>
		/// Снятие (сокрытие) билета
		/// </summary>
		Trash = 1 << 3,

		/// <summary>
		/// Блокировка (бан) билета
		/// </summary>
		Block = 1 << 4,

		/// <summary>
		/// Восстановление билета
		/// </summary>
		Recovery = 1 << 5,

		/// <summary>
		/// Удаление билета
		/// </summary>
		Delete = 1 << 6
	}
}