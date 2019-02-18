using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using EasyNetQ;
using Notification.Logic;
using Notification.Logic.MappingProfiles;
using Notification.Web.Subscriber;
using Notification.Logic.Configs;
using Notification.Web.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using Notification.Web.Validators.Filters;

namespace Notification.Web
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
			services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<NotificationMessageProfile>();
				cfg.AddProfile<UserNotificationProfile>();
				cfg.AddProfile<EmailNotificationProfile>();
                cfg.AddProfile<Logic.MappingProfiles.SubscriptionProfile>();
                cfg.AddProfile<Logic.MappingProfiles.SeverityProfile>();
                cfg.AddProfile<Web.MappingProfiles.SeverityProfile>();
			});

            services.AddCors();

            services.AddMvc(opts =>
                {
                    opts.Filters.Add(typeof(ValidationFilterAttribute));
                    opts.Filters.Add(new NotificationNotFoundExceptionFilter());
                    opts.Filters.Add(new UserNotFoundExceptionFilter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSingleton<IValidatorFactory, ValidatorFactory>();

            services.AddSwaggerDocument();

			services.Configure<SenderConfig>(Configuration.GetSection("SenderConfig"));
			services.RegisterNotificationContext();
			services.RegisterNotificationService();
			services.RegisterEmailNotificationService();
			services.RegisterEmailSenderService();
            services.RegisterSubscriptionService();
            services.RegisterUserService();
            
            //services.RegisterNotificationMessageFilterValidator();

            services.AddSingleton<IBus>(RabbitHutch.CreateBus("host=rabbit"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();                
            }
            else
            {
                app.UseSubscriber("Notification", Assembly.GetExecutingAssembly());
            }

            app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseSwagger().UseSwaggerUi3();

			app.UseMvc();			
		}
	}
}
