using UserManagement.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EasyNetQ;
using System.Reflection;
using UserManagement.Logic.Subscriber;
using EasyNetQ.ConnectionString;

namespace UserManagement.Web
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
            //services.AddAuthentication().AddIdentityServerAuthentication();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerDocument(settings => settings.Title = "User Management");
            services.RegisterUserServices();
            services.AddOptions();
            services.AddSingleton(Configuration);
            var value = Configuration["Hostname"];
            services.AddSingleton(RabbitHutch.CreateBus(value));
            services.AddCors(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseSwagger().UseSwaggerUi3();
            app.UseSubscriber("UserSettingsService", Assembly.GetExecutingAssembly());
            //app.UseAuthentication();
            app.UseMvc();
        }
    }
}
