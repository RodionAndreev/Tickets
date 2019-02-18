using System.Threading.Tasks;
using Notification.Logic.Configs;
using Notification.Logic.Models.Requests;

namespace Notification.Logic.Services
{
	/// <summary>
	/// Интерфейс отправки сообщений по почте
	/// </summary>
	public interface IEmailSenderService
	{
		/// <summary>
		/// Отправить сообщение
		/// </summary>
		/// <param name="request">Запрос на сообщение</param>
		Task SendEmailAsync(SendEmailRequest request, SenderConfig senderConfig);
	}
}
