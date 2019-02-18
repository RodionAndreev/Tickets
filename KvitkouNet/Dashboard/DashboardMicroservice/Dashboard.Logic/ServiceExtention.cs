using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Dashboard.Data.Context;
using Dashboard.Data.Repositories;
using Dashboard.Logic.Services;
using Dashboard.Logic.Validators;
using Dashboard.Logic.MappingProfiles;
using Dashboard.Data;
using Dashboard.Data.Factories;

namespace Dashboard.Logic
{
    public static class ServiceExtentions
    {
        /// <summary>
        ///     Регистрация IDashboardService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDashboardService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DashboardContext>(opt => opt.UseSqlite(connectionString));

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<NewsProfile>();
                cfg.AddProfile<TicketInfoProfile>();
            });
            
            services.AddScoped<IValidator<Models.News>, NewsValidator>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.RepositoryContext(connectionString);

            return services;

        }

        #region Tests
        private static Mock<IDashboardService> DashboardServiceMock()
        {
            var newsServiceMock = new Mock<IDashboardService>();


            return newsServiceMock;
        }
        #endregion
    }
}