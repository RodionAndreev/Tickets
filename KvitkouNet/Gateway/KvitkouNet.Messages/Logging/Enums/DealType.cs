using System;

namespace KvitkouNet.Messages.Logging.Enums
{
	/// <summary>
	/// Перечисление, описывающее тип сделки по билету
	/// </summary>
	[Flags]
	public enum DealType
	{
		/// <summary>
		/// Неизвестный тип
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Продажа билета
		/// </summary>
		Selling = 1 << 0,

		/// <summary>
		/// Обмен билетами
		/// </summary>
		Swap = 1 << 1,

		/// <summary>
		///  Дарение билета
		/// </summary>
		Grant = 1 << 2
	}
}