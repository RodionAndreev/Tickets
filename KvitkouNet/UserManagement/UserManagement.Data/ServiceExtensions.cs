using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using UserManagement.Data.Context;
using UserManagement.Data.DbModels;
using UserManagement.Data.Fakers;
using UserManagement.Data.Repositories;

namespace UserManagement.Data
{
    public static class ServiceExtentions
    {
        public static IServiceCollection RegisterUserServicesData(this IServiceCollection services)
        {
            services.AddDbContext<UserContext>(opt => opt.UseLazyLoadingProxies().UseSqlite("Data Source=./UserDatabase.db"))
                .AddIdentity<UserDB, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();
            var o = new DbContextOptionsBuilder<UserContext>();
            o.UseLazyLoadingProxies().UseSqlite("Data Source=./UserDatabase.db");
            
            using (var context = new UserContext(o.Options))
            {
                context.Database.EnsureCreated();
                if (!context.Users.Any())
                {
                    context.Users.AddRange(UserFaker.Generate());
                    context.SaveChanges();
                }
            }
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
