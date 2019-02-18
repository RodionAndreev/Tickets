using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notification.Data.Context;
using Notification.Logic.MappingProfiles;
using System;

namespace Notification.Test
{
    class BaseTest : IDisposable
    {
        protected readonly NotificationContext Context;
        protected readonly IMapper Mapper;

        public BaseTest()
        {
            var options = new DbContextOptionsBuilder<NotificationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new NotificationContext(options);

            Context.Database.EnsureCreated();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<NotificationMessageProfile>();
                cfg.AddProfile<UserNotificationProfile>();
                cfg.AddProfile<EmailNotificationProfile>(); ;
            });

            Mapper = mockMapper.CreateMapper();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
