using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dashboard.Logic;
using EasyNetQ;
using Dashboard.Subscriber;
using System.Reflection;

namespace DashboardMicroService
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
            var connectionString = Configuration["connectionString"];

            var rabbitConnectionString = Configuration["RabbitConnection"];

            services.RegisterDashboardService(connectionString);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                        
            services.AddSwaggerDocument(settings => settings.Title = "Dashboard");

            //services.AddSingleton<IBus>(RabbitHutch.CreateBus(rabbitConnectionString));

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())app.UseDeveloperExceptionPage();

            app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                        
            app.UseSwagger().UseSwaggerUi3();

            app.UseMvc();

           // app.UseSubscriber("TicketService", Assembly.GetExecutingAssembly());
        }
    }
}
