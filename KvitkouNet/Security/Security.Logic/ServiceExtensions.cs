using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Data;
using Security.Logic.Implementations;
using Security.Logic.MappingProfiles;
using Security.Logic.Models;
using Security.Logic.Models.Requests;
using Security.Logic.Services;
using Security.Logic.Validators;

namespace Security.Logic
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Регистрация ISecurityService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSecurityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterSecurityData(configuration);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AccessRightProfile>();
                cfg.AddProfile<AccessFunctionProfile>();
                cfg.AddProfile<FeatureProfile>();
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<UserRightsProfile>();
                cfg.AddProfile<UserInfoProfile>();
            });

            services.AddScoped<IValidator<AccessRight>, AccessRightValidator>();
            services.AddScoped<IValidator<AccessFunction>, AccessFunctionValidator>();
            services.AddScoped<IValidator<Feature>, FeatureValidator>();
            services.AddScoped<IValidator<Role>, RoleValidator>();
            services.AddScoped<IValidator<UserRights>, UserRightsValidator>();
            services.AddScoped<IValidator<CheckAccessRequest>, AccessRequestValidator>();

            services.AddScoped<IRightsService, RightsService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IFunctionService, FunctionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRightsService, UserRightsService>();

            services.AddMediatR();

            return services;
        }
    }
}
