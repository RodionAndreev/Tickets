using UserManagement.Logic.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using FluentValidation;
using UserManagement.Logic.Validators;
using UserManagement.Logic.Models;
using AutoMapper;
using UserManagement.Logic.MappingProfiles;
using UserManagement.Data;
using UserManagement.Logic.Subscriber;
using KvitkouNet.Messages.UserManagement;
using EasyNetQ.AutoSubscribe;

namespace UserManagement.Logic
{
    public static class ServiceExtentions
    {
        public static IServiceCollection RegisterUserServices(this IServiceCollection services)
        {
            services.RegisterUserServicesData();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidator<UserRegisterModel>, UserRegisterValidator>();
            //services.AddScoped<IValidator<User>, UserValidator>();
            //services.AddScoped<IValidator<Account>, AccountValidator>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ForViewModelProfile>();
                cfg.AddProfile<ForUpdateModelProfile>();
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<UserRegisterProfile>();
                cfg.AddProfile<ProfileProfile>();

            });
            services.AddScoped<UserSettingsMessageConsumer>();
            //services.AddScoped<IConsumeAsync<AccountMessage>, UserSettingsMessageConsumer>();

            return services;
        }
    }
}
