using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Search.Data.Models;
using Search.Logic.Common.Models;
using Search.Logic.Services;

namespace Search.Logic
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ISearchUserService, SearchUserService>();
            services.AddScoped<ISearchTicketService, SearchTicketService>();
            services.AddScoped<ISearchHistoryService, SearchHistoryService>();
            return services;
        }
    }
}
