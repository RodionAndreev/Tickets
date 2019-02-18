using AutoMapper;
using Chat.Data.Context;
using Chat.Data.Helpers;
using Chat.Logic.MappingProfiles;
using Chat.Logic.Models;
using Microsoft.Extensions.DependencyInjection;
using Chat.Logic.Services;
using Chat.Logic.Validators;
using FluentValidation;

namespace Chat.Logic
{
    public static class ServiceExtentions
    {
        /// <summary>
        /// Регистрация IChatService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterChatService(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IValidator<Settings>, SettingsValidator>();
            return services;
        }

        /// <summary>
        /// Регистрация IRoomService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterRoomService(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IValidator<Room>, RoomValidator>();
            return services;
        }

        /// <summary>
        /// Регистрация DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ChatContext>(new RegisterContextHelper().GetOptionsBuilder());
            return services;
        }

        /// <summary>
        /// Регистрация AutoMapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAutoMapperLogic(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MessageProfile>();
                cfg.AddProfile<RoomProfile>();
                cfg.AddProfile<SettingsProfile>();
                cfg.AddProfile<UserProfile>();

            });
            return services;
        }
    }
}

