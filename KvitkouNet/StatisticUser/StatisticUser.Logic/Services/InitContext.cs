using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using StatisticUser.Data;
using StatisticUser.Data.Fakers;

namespace StatisticUser.Logic.Services
{
    public static class InitContext
    {
        /// <summary>
        /// Метод для инициализации WebApiContext
        /// </summary>
        public static void InitializeContext(IServiceProvider serviceProvider)
        {
            const string errorMessage = "Failed to initialize context.";
            try
            {
                var context = serviceProvider.GetRequiredService<WebApiContext>();
                context.Database.Migrate();
                if (!context.SummaryTable.Any())
                {
                    context.SummaryTable.AddRange(StatisticUserFaker.Generate(500));
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
