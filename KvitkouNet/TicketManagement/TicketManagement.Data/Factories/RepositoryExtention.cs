using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Data.Context;
using TicketManagement.Data.Fakes;

namespace TicketManagement.Data.Factories
{
    public static class RepositoryExtention
    {
        public static IServiceCollection RepositoryContext(this IServiceCollection collection, string connectionString)
        {
            var o = new DbContextOptionsBuilder<TicketContext>();
            o.UseSqlite(connectionString);
            using (var ctx = new TicketContext(o.Options))
            {
                ctx.Database.Migrate();
                if (!ctx.Tickets.Any())
                {
                    ctx.Tickets.AddRange(TicketFaker.Generate(50));
                    ctx.SaveChanges();
                }
            }

            return collection;
        }
    }
}