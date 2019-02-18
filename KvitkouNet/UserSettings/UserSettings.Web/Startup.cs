using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserSettings.Logic;
using EasyNetQ;

namespace UserSettings.Web
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddOptions();
			services.AddSingleton(Configuration);
			var value = Configuration["Hostname"];
			var connectionStringDb = Configuration["connectionString"];
			services.AddSwaggerDocument(setting => setting.Title = "User Setting");

			services.AddSingleton(RabbitHutch.CreateBus("host=rabbit"));
			services.RegisterUserSettingsService(connectionStringDb);
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
			app.UseSubscriber();
			app.UseMvc();
		}
	}
}
