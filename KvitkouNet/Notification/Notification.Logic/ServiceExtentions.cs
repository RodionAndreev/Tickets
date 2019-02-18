using Microsoft.Extensions.DependencyInjection;
using Notification.Logic.Services;
using Notification.Data.Context;
using Notification.Data.Helpers;
using Notification.Logic.Services.NotificationService;
using Notification.Logic.Services.EmailNotificationService;
using Notification.Logic.Services.EmailSenderService;
using Notification.Logic.Services.Interfaces;
using Notification.Logic.Services.SubscriptionService;
using Notification.Logic.Services.UserServices;
using FluentValidation;

namespace Notification.Logic
{
    public static class ServiceExtentions
    {
		/// <summary>
		/// Регистрация INotificationService
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterNotificationService(this IServiceCollection services)
		{
			return services.AddScoped<INotificationService, NotificationService>();
        }

		/// <summary>
		/// Регистрация IEmailNotificationService
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterEmailNotificationService(this IServiceCollection services)
		{
			return services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        }

		/// <summary>
		/// Регистрация IEmailSenderService
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterEmailSenderService(this IServiceCollection services)
		{
			return services.AddScoped<IEmailSenderService, EmailSenderService>();
        }

		/// <summary>
		/// Регистрация NotificationContext
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterNotificationContext(this IServiceCollection services)
		{			
			return services.AddDbContext<NotificationContext>(new RegisterContextHelper().GetOptionsBuilder());
        }

        /// <summary>
        /// Регистрация ISubscriptionService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSubscriptionService(this IServiceCollection services)
        {
            return services.AddScoped<ISubscriptionService, SubscriptionService>();
        }

        /// <summary>
        /// Регистрация ISubscriptionService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterUserService(this IServiceCollection services)
        {
            return services.AddScoped<IUserService, UserServices>();
        }
    }
}
