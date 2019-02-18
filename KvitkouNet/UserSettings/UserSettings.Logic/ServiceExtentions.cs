using AutoMapper;
using EasyNetQ.AutoSubscribe;
using FluentValidation;
using KvitkouNet.Messages.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using UserSettings.Data;
using UserSettings.Data.Context;
using UserSettings.Logic.MappingProfile;
using UserSettings.Logic.Models;
using UserSettings.Logic.Services;
using UserSettings.Logic.Validators;

namespace UserSettings.Logic
{
	public static class ServiceExtentions
	{
		/// <summary>
		/// Регистрация IUserSettingsService
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterUserSettingsService(this IServiceCollection services, string connetctionString)
		{
			services.RegisterDbService(connetctionString);
			services.AddScoped<IConsumeAsync<UserCreationMessage>, UserProfileConsumer>();
			services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<SettingsProfile>();
				cfg.AddProfile<NotificationsProfile>();
			});
			services.AddScoped<IValidator<Settings>, SettingsValidator>();
			services.AddScoped<IUserSettingsService, UserSettingsService>();
			services.AddScoped<ISettingsRepo, SettingsRepo>();

			return services;
		}
		public static IServiceCollection RegisterDbService(this IServiceCollection services, string connetctionString)
		{
			services.AddDbContext<SettingsContext>(
					opt => opt.UseSqlite(connetctionString));
			var o = new DbContextOptionsBuilder<SettingsContext>();
			o.UseSqlite(connetctionString);
			using (var ctx = new SettingsContext(o.Options))
			{
				ctx.Database.Migrate();
				if (!ctx.Settings.Any())
				{

					ctx.SaveChanges();
				}
			}
			return services;
		}
	}
}
