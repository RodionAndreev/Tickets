using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using StatisticOnline.Data.Context;
using StatisticOnline.Data.Fakers;


namespace StatisticOnline.Logic.Services
{
    public static class ContextInit
    {
        /// <summary>
        /// Метод для инициализации WebApiContext
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <exception cref="DataException">ошибка при инициализации контекста </exception>

        public static void InitContext(this IServiceProvider serviceProvider)
        {

            const string errorMessage = "Failed to initialize context.";
            try
            {
                var context = serviceProvider.GetService<WebApiContext>();
                context.Database.Migrate();
                if (!context.StatisticOnline.Any())
                {
                    context.StatisticOnline.AddRange(StatisticOnlineFaker.Generate(150));
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new DataException(errorMessage);
            }
        }
    }
}
