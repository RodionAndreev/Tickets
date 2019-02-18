using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Search.Data.Repositories;

namespace Search.Data
{
    public static class RepositoryExtension
    {
        public static IServiceCollection RegisterHistoryRepository(this IServiceCollection services)
        {
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<ITicketRepository, ElasticSearchTicketRepository>();
            services.AddScoped<IUserRepository, ElasticSearchUserRepository>();

            return services;
        }
    }
}
