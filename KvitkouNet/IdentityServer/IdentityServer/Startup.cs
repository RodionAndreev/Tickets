using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.SecurityClient.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer
{
	public class Startup
	{
		public IHostingEnvironment Environment { get; }

		public IConfiguration Configuration { get; }

		public Startup(IHostingEnvironment environment, IConfiguration configuration)
		{
			Environment = environment;
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
		    services.AddScoped<IUserRightsApi, UserRightsApi>();

            services.AddCors();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.Configure<IISOptions>(options =>
			{
				options.AutomaticAuthentication = false;
				options.AuthenticationDisplayName = "Windows";
			});
            var builder = services.AddIdentityServer(options =>
				{
					options.IssuerUri = "http://identityserver";
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;
				}).AddTestUsers(AuthStorage.Users);

			builder.AddInMemoryIdentityResources(AuthStorage.GetIdentityResources());
			builder.AddInMemoryApiResources(AuthStorage.GetApis());
			builder.AddInMemoryClients(AuthStorage.GetClients());
            
            services.AddIdentity<IdentityUser, IdentityRole>()
		        .AddUserManager<CustomUserManager>();

			builder.AddDeveloperSigningCredential();
			services.AddAuthentication();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(_ => _.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseIdentityServer();
			app.UseMvc();
		}
	}
}
