using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using Notification.Logic.Models.Requests;
using Notification.Logic.Configs;

namespace Notification.Logic.Services.EmailSenderService
{
	public class EmailSenderService : IEmailSenderService
	{
		public async Task SendEmailAsync(SendEmailRequest request, SenderConfig senderConfig)
		{
			MimeMessage emailMessage = new MimeMessage();

			emailMessage.From.Add(new MailboxAddress(senderConfig.Name, senderConfig.Email));
			emailMessage.To.Add(new MailboxAddress(request.ReceiverName, request.ReceiverEmail));
			emailMessage.Subject = request.Subject;

			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = request.Text
			};

			using (SmtpClient client = new SmtpClient())
			{
				try
				{
					await client.ConnectAsync("smtp.gmail.com", 587, false);
					await client.AuthenticateAsync(senderConfig.Email, senderConfig.Password);
					await client.SendAsync(emailMessage);
					await client.DisconnectAsync(true);
				}
				catch //нужно придумать что обработать. может отдельные ошибки на каждый вызов
				{
					throw;
				}				
			}
		}
	}
}
