
using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StatisticOnline.Data.Context;
using StatisticOnline.Logic.Interfaces;
using StatisticOnline.Logic.MappingProfiles;
using StatisticOnline.Logic.Models;
using StatisticOnline.Logic.Validators;

namespace StatisticOnline.Logic.Services
{
    public static class ServiceExtentions
    {
        public static IServiceCollection StatisticOnlineServicesMoq(this IServiceCollection services)
        {
            var mock = new Mock<IStatisticOnlineService>();

            mock.Setup(_ => _.GetAllUsers())
                .ReturnsAsync(new OnlineModel() { CountRegistered = 80, CountAll = 100, CountGuest = 20, Id = 0, CreateTime = DateTime.Now});

            mock.Setup(_ => _.GetDateRangeUsers(It.IsAny<DateRange>()))
                .ReturnsAsync(new List<OnlineModel>()
                {
                    new OnlineModel() {CountRegistered = 80, CountAll = 100, CountGuest = 20, Id = 103, CreateTime = DateTime.Now},
                    new OnlineModel() {CountRegistered = 75, CountAll = 90, CountGuest = 15, Id = 102, CreateTime = DateTime.Now.AddHours(-1)},
                    new OnlineModel() {CountRegistered = 70, CountAll = 95, CountGuest = 25, Id = 101, CreateTime = DateTime.Now.AddHours(-2)},
                    new OnlineModel() {CountRegistered = 72, CountAll = 91, CountGuest = 19, Id = 100, CreateTime = DateTime.Now.AddHours(-3)}
                });

            mock.Setup(_ => _.GetRegisteredUser())
                .ReturnsAsync(27);

            mock.Setup(_ => _.GetGuestUser())
                .ReturnsAsync(48);

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<StatisticOnlineProfile>();
            });


            services.AddScoped<IStatisticOnlineService>(_ => mock.Object);
            return services;
        }

        /// <summary>
        /// Добавление WebApiContext
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            const string connectionString = "Data Source=StatisticOnline.db";
            services.AddDbContext<WebApiContext>(opt => opt.UseSqlite(connectionString));
            return services;
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<DateRange>, RangeVaridator>();

            return services;
        }

        public static IServiceCollection StatisticService(this IServiceCollection services)
        {
            services.AddScoped<IStatisticOnlineService, StatisticOnlineService>();

            return services;
        }


    }
}
