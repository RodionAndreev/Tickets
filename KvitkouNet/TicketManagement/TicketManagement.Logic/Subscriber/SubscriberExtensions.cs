using System;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;
using KvitkouNet.Messages.UserSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace TicketManagement.Logic.Subscriber
{
    public static class SubscriberExtensions
    {
        public static IApplicationBuilder UseSubscriber(this IApplicationBuilder app)
        {
            var userUpdatedPreffix = "UserForTicket.Updated";
            var userSetUpdatedPreffix = "UserSettingsForTicket.Updated";
            var userDeletedPreffix = "UserForTicket.Deleted";
            var userSetDeletedPreffix = "UserSettingsForTicket.Deleted";
            var services = app.ApplicationServices.CreateScope().ServiceProvider;
            var policy = Policy.Handle<TimeoutException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1)
                });
            var lifetime = services.GetService<IApplicationLifetime>();
            var bus = services.GetService<IBus>();

            lifetime.ApplicationStarted.Register(() =>
            {
                try
                {
                    bus.SubscribeAsync<UserDeletedMessage>(userDeletedPreffix,
                        msg => services.GetService<IConsumeAsync<UserDeletedMessage>>().ConsumeAsync(msg));

                    bus.SubscribeAsync<UserUpdatedMessage>(userUpdatedPreffix,
                        msg => services.GetService<IConsumeAsync<UserUpdatedMessage>>().ConsumeAsync(msg));

                    bus.SubscribeAsync<UserProfileUpdateMessage>(userSetUpdatedPreffix,
                        msg => services.GetService<IConsumeAsync<UserProfileUpdateMessage>>().ConsumeAsync(msg));

                    bus.SubscribeAsync<DeleteUserProfileMessage>(userSetDeletedPreffix,
                        msg => services.GetService<IConsumeAsync<DeleteUserProfileMessage>>().ConsumeAsync(msg));
                }
                catch (TimeoutException)
                {
                    //used to bypass RabbitMq subscription error
                }
            });

            lifetime.ApplicationStopped.Register(() => bus.Dispose());

            return app;
        }
    }
}