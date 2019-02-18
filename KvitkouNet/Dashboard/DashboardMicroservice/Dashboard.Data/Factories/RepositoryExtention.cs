using Dashboard.Data.Context;
using Dashboard.Data.Fakers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard.Data.Factories
{
    public static class RepositoryExtention
    {

        public static IServiceCollection RepositoryContext(this IServiceCollection collection, string connectionString)
        {
            var o = new DbContextOptionsBuilder<DashboardContext>();
            o.UseSqlite(connectionString);

            using (var context = new DashboardContext(o.Options))
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
                if (!context.News.Any())
                {
                    context.News.AddRange(NewsFaker.Generate(15));
                    context.SaveChanges();
                }
            }

            return collection;
        }
    }
}
