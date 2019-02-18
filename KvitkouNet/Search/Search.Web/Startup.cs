using System.Reflection;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Search.Data;
using Search.Data.Context;
using Search.Logic;
using Search.Logic.MappingProfiles;
using Search.Web.Filters;
using Search.Web.MappingProfiles;
using Search.Web.Subscriber;

namespace Search.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            string elasticSearchConnectionString = Configuration.GetConnectionString("ElasticSearchConnection");
            string rabbitConnectionString = Configuration.GetConnectionString("RabbitConnection");

            services.AddDbContext<SearchContext>(
                opt => opt.UseSqlite(connectionString: connectionString));

            var o = new DbContextOptionsBuilder<SearchContext>();
            o.UseSqlite(connectionString);

            using (var ctx = new SearchContext(o.Options))
            {
                ctx.Database.Migrate();
            }

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<SearchProfile>();
                cfg.AddProfile<TicketProfile>();
                cfg.AddProfile<UserProfile>();
            });

            services.AddMvc(opt => opt.Filters.Add(typeof(ExceptionFilter)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerDocument(opt => opt.Title = "SearchService");
            services.RegisterElasticSearch(elasticSearchConnectionString);
            services.RegisterHistoryRepository();
            services.RegisterServices();
            services.RegisterConsumers();

            services.AddSingleton<IBus>(RabbitHutch.CreateBus(rabbitConnectionString));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    //.WithOrigins("http://localhost:4200")
                    .AllowAnyOrigin());

            app.UseSwagger().UseSwaggerUi3();
            app.UseMvc();

            app.UseSubscriber("TicketService", Assembly.GetExecutingAssembly());
            app.UseSubscriber("UserService", Assembly.GetExecutingAssembly());
        }
    }
}
