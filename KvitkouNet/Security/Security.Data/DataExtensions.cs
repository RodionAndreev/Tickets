using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Data.ConfigModels;
using Security.Data.Context;
using Security.Data.MapperProfiles;

namespace Security.Data
{
    public static class DataExtensions
    {
        /// <summary>
        /// for local ef tests
        /// </summary>
        /// <returns></returns>
        public static ISecurityData GetISecurityData()
        {
            var o = new DbContextOptionsBuilder<SecurityContext>();
            o.UseSqlite("Data Source=D:\\gitRep\\kvitkou-net\\KvitkouNet\\Security\\Security.Web\\SecurityDatabase.db");
            using (var ctx = new SecurityContext(o.Options))
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }

            return new SecurityData(new SecurityContext(o.Options),
                new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AccessRightProfile>();
                    cfg.AddProfile<AccessFunctionProfile>();
                    cfg.AddProfile<FeatureProfile>();
                    cfg.AddProfile<RoleProfile>();
                    cfg.AddProfile<UserRightsProfile>();
                    cfg.AddProfile<UserInfoProfile>();
                })));
        }

        /// <summary>
        /// Регистрация DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSecurityData(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AccessRightProfile>();
                cfg.AddProfile<AccessFunctionProfile>();
                cfg.AddProfile<FeatureProfile>();
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<UserRightsProfile>();
                cfg.AddProfile<UserInfoProfile>();
            });

            var o = new DbContextOptionsBuilder<SecurityContext>();
            o.UseSqlite("Data Source=./SecurityDatabase.db");

            using (var ctx = new SecurityContext(o.Options))
            {
                if (ctx.Database.EnsureCreated())
                {
                    var senderConfig = configuration.GetSection("DefaultRulesAll").Get<DefaultRulesAll>();

                    DataBaseHelper.CreateDefault(senderConfig, new SecurityData(ctx, new Mapper(new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<AccessRightProfile>();
                        cfg.AddProfile<AccessFunctionProfile>();
                        cfg.AddProfile<FeatureProfile>();
                        cfg.AddProfile<RoleProfile>();
                        cfg.AddProfile<UserRightsProfile>();
                        cfg.AddProfile<UserInfoProfile>();
                    }))));
                }
            }

            services.AddDbContext<SecurityContext>(
                opt => opt.UseSqlite("Data Source=./SecurityDatabase.db"));


            services.AddScoped<ISecurityData, SecurityData>();

            return services;
        }
    }
}
