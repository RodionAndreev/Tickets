using System;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Search.Data
{
    public static class ElasticSearchExtension
    {
        public static IServiceCollection RegisterElasticSearch(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IElasticClient>(provider =>
                new ElasticClient(new ConnectionSettings(new Uri(connectionString))));

            return services;
        }
    }
}
