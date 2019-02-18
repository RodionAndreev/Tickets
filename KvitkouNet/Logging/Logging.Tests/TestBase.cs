using System;
using AutoMapper;
using Logging.Data;
using Logging.Logic.MappingProfiles.DomainToDbReverse;
using Microsoft.EntityFrameworkCore;

namespace Logging.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly LoggingDbContext Context;
        protected readonly IMapper Mapper;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<LoggingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new LoggingDbContext(options);

            Context.Database.EnsureCreated();
            
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(AccountLogDomainToDbProfile).Assembly);
            });

            Mapper = mockMapper.CreateMapper();
        }

        public static DateTime DateFrom { get; } = DateTime.Now.AddMinutes(-1);

        public static DateTime DateTimeNow { get; } = DateTime.Now;

        public static DateTime DateTo { get; } = DateTime.Now.AddMinutes(1);

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}
