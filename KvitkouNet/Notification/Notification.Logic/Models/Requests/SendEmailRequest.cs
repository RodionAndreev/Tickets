namespace Notification.Logic.Models.Requests
{
	/// <summary>
	/// Запрос для отправки сообщения на почту
	/// </summary>
	public class SendEmailRequest
	{
		/// <summary>
		/// Имя получателя
		/// </summary>
		public string ReceiverName { get; set; }

		/// <summary>
		/// Почта получателя
		/// </summary>
		public string ReceiverEmail { get; set; }

		/// <summary>
		/// Тема сообщения
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// Текст сообщения
		/// </summary>
		/// <remarks>Поддерживает html</remarks>
		public string Text { get; set; }

	}
}
