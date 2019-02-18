using System;
using System.Linq;
using System.Reflection;
using AdminPanel.Web.Extensions;
using AdminPanel.Web.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminPanel.Web
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
			var assemblyNamesToScan = Assembly
				.GetEntryAssembly()
				.GetReferencedAssemblies()
				.Where(an => an.FullName.StartsWith("AdminPanel", StringComparison.OrdinalIgnoreCase))
				.Select(an => an.FullName);

			services.AddAutoMapper(cfg => cfg.AddProfiles(assemblyNamesToScan));

			services.AddMvc(cfg => cfg.Filters.Add(new PollyActionFilter())).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddSwaggerDocument();

			services.RegisterUserService();
			services.RegisterLoggingServices();
			services.RegisterFilters();
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
		}
	}
}
