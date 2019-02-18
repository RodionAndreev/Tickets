using AutoMapper;
using EasyNetQ.AutoSubscribe;
using FluentValidation;
using KvitkouNet.Messages.UserManagement;
using KvitkouNet.Messages.UserSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Data.Context;
using TicketManagement.Data.Factories;
using TicketManagement.Data.Repositories;
using TicketManagement.Logic.MappingProfiles;
using TicketManagement.Logic.Models;
using TicketManagement.Logic.Services;
using TicketManagement.Logic.Subscriber;
using TicketManagement.Logic.Validators;

namespace TicketManagement.Logic.Extentions
{
    /// <summary>
    ///     Сервис для регистрации сущностей в di
    /// </summary>
    public static class ServiceExtentions
    {
        /// <summary>
        ///     Регистрация ITicketService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterTicketService(this IServiceCollection services,
            string connetctionString)
        {
            services.AddDbContext<TicketContext>(opt => opt.UseSqlite(connetctionString));
            services.AddScoped<IValidator<Ticket>, TicketValidator>();
            services.AddScoped<IValidator<UserInfo>, UserValidator>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();
            services.RepositoryContext(connetctionString);
            services.AddScoped<Data.DbModels.Page<Data.DbModels.Ticket>>();
            services.AddScoped<IConsumeAsync<UserUpdatedMessage>, UserUpdateMessageConsumer>();
            services.AddScoped<IConsumeAsync<UserDeletedMessage>, UserDeleteMessageConsumer>();
            services.AddScoped<IConsumeAsync<UserProfileUpdateMessage>, UserUpdateMessageConsumerFromSettings>();
            services
                .AddScoped<IConsumeAsync<DeleteUserProfileMessage>, UserDeleteMessageConsumerFromSettings
                >();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<TicketProfile>();
                cfg.AddProfile<AddressProfile>();
                cfg.AddProfile<UserInfoProfile>();
            });
            return services;
        }
    }
}