using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;

namespace KvitkouNet.Web
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
			services.AddCors();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});

			services.AddSwaggerDocument();

            //this need for Hub
            services.AddOcelot();
            services.AddWebSockets(opt => opt.AllowedOrigins.Add("*"));

			services.AddAuthentication()
				.AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme,
					opt =>
					{
						opt.Authority = "http://identityserver";
						opt.RequireHttpsMetadata = false;
						opt.IncludeErrorDetails = true;
						opt.TokenValidationParameters.NameClaimType = JwtClaimTypes.Name;
						opt.TokenValidationParameters.RoleClaimType = JwtClaimTypes.Role;
						opt.TokenValidationParameters.ValidIssuer = "http://identityserver";
						opt.TokenValidationParameters.ValidAudience = "http://identityserver/resources";
						opt.TokenValidationParameters.ValidateIssuer = true;
						opt.TokenValidationParameters.ValidateAudience = true;
						opt.TokenValidationParameters.ValidateIssuerSigningKey = false;
						opt.Validate();
					}, null);

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
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
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			//app.UseHttpsRedirection();
			app.UseStaticFiles();
			//app.UseSpaStaticFiles();

			app.UseSwagger().UseSwaggerUi3();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501
            //
            //    spa.Options.SourcePath = "ClientApp";
            //
            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});

		    //this need for Hub
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyHeader()
                .AllowAnyMethod());
            app.UseOcelot().GetAwaiter().GetResult();
        }
    }
}