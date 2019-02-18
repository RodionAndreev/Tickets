using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StatisticUser.Data;
using StatisticUser.Logic.DTOs;
using StatisticUser.Logic.Interfaces;

namespace StatisticUser.Logic.Services
{
    public static class ServiceExtentions
    {

        /// <summary>
        /// Moq объекта StatisticUserServices
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection StatisticUserServicesMoq(this IServiceCollection services)
        {
            var mock = new Mock<IStatisticUserService>();

            services.AddScoped<IStatisticUserService>(_ => mock.Object);
            return services;
        }

        /// <summary>
        /// StatisticUserServices
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection StatisticUserServices(this IServiceCollection services)
        {
            services.AddScoped<IStatisticUserService, StatisticUserService>();
            return services;
        }

        /// <summary>
        /// Добавление WebApiContext
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            const string connectionString = "Data Source=StatisticUsers.db";
            services.AddDbContext<WebApiContext>(opt => opt.UseSqlite(connectionString));
            return services;
        }
    }
}
