using System.Net.Http;
using AdminPanel.Logic.Generated.Logging;
using AdminPanel.Logic.Generated.UserManagement;
using AdminPanel.Logic.Infrastructure;
using AdminPanel.Logic.Services;
using AdminPanel.Web.Filters;
using Microsoft.Extensions.DependencyInjection;
using AccountLog = AdminPanel.Logic.Generated.Logging.AccountLog;
using ErrorLog = AdminPanel.Logic.Generated.Logging.ErrorLog;
using IAccountLog = AdminPanel.Logic.Generated.Logging.IAccountLog;
using IErrorLog = AdminPanel.Logic.Generated.Logging.IErrorLog;
using IPaymentLog = AdminPanel.Logic.Generated.Logging.IPaymentLog;
using IQueryLog = AdminPanel.Logic.Generated.Logging.IQueryLog;
using ITicketActionLog = AdminPanel.Logic.Generated.Logging.ITicketActionLog;
using ITicketDealLog = AdminPanel.Logic.Generated.Logging.ITicketDealLog;
using PaymentLog = AdminPanel.Logic.Generated.Logging.PaymentLog;
using QueryLog = AdminPanel.Logic.Generated.Logging.QueryLog;
using TicketActionLog = AdminPanel.Logic.Generated.Logging.TicketActionLog;
using TicketDealLog = AdminPanel.Logic.Generated.Logging.TicketDealLog;
using Users = AdminPanel.Logic.Generated.Logging.Users;
using UserService = AdminPanel.Logic.Generated.Logging.UserService;

namespace AdminPanel.Web.Extensions
{
	public static class ServiceExtensions
	{
		/// <summary>
		/// Регистрация IUserService
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterUserService(this IServiceCollection services)
		{
			services.AddScoped<IUserServiceWrapper, UserServiceWrapper>();
			services.AddScoped<IUser>(p => new User(new UserTitle(new HttpClient(), true)));
			return services;
		}

		/// <summary>
		/// Регистрация сгенерированных сервисов
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterLoggingServices(this IServiceCollection services)
		{
			services.AddScoped<IErrorLog>(p => new ErrorLog(new MyTitle(new HttpClient(), true)));
			services.AddScoped<IAccountLog>(p => new AccountLog(new MyTitle(new HttpClient(), true)));
			services.AddScoped<IPaymentLog>(p => new PaymentLog(new MyTitle(new HttpClient(), true)));
			services.AddScoped<IQueryLog>(p => new QueryLog(new MyTitle(new HttpClient(), true)));
			services.AddScoped<ITicketActionLog>(p => new TicketActionLog(new MyTitle(new HttpClient(), true)));
			services.AddScoped<ITicketDealLog>(p => new TicketDealLog(new MyTitle(new HttpClient(), true)));

			return services;
		}


		public static IServiceCollection RegisterFilters(this IServiceCollection services)
		{
			services.AddScoped<GlobalExceptionFilter>();

			return services;
		}
	}
}