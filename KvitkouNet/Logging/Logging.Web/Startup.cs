using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Logging.Logic.Extensions;
using Logging.Web.Extensions;
using Logging.Web.Subscriber;
using Logging.Web.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logging.Web
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
			services.RegisterDbContext();

			services
			    .AddMvc(opts => opts.Filters.Add(typeof(ValidationFilterAttribute)))
			    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
			    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

		    services.AddSingleton<IValidatorFactory, ValidatorFactory>();

            services.AddSwaggerDocument();

			services.RegisterServices();

			services.RegisterAutoMapper();

			services.RegisterConsumers();

			services.RegisterEasyNetQ("host=rabbit");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger().UseSwaggerUi3();

			app.UseMvc();

			app.UseSubscriber("ErrorLogging", Assembly.GetExecutingAssembly());
		}
	}
}
