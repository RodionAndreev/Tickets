using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssert;
using Logging.Data.DbModels;
using Logging.Data.Fakers;
using Logging.Logic.Enums;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;
using Logging.Logic.Models.Filters;
using Logging.Logic.Services;
using Xunit;

namespace Logging.Tests
{
    public class TicketLogServiceTests : TestBase
    {
        private readonly ITicketLogService _ticketLogService;

        public TicketLogServiceTests()
        {
            _ticketLogService = new TicketLogService(context: Context, mapper: Mapper);
        }

        [Fact]
        public async Task ShouldAddLogEntry()
        {
            // Arrange
            var entry = new TicketActionLogEntry
            {
                UserId = "alexpvt",
                TicketId = "ticket",
                TicketName = "opera aida",
                ActionType = TicketActionType.Add,
                Description = "TicketDescription",
                EventDate = DateTimeNow
            };

            // Act
            await _ticketLogService.AddLogAsync(entry);

            // Assert
            var dbEntry = Context.TicketActionLogEntries.Single();

            dbEntry.Id.ShouldNotBeNullOrEmpty();
            dbEntry.UserId.ShouldBeEqualTo("alexpvt");
            dbEntry.TicketId.ShouldBeEqualTo("ticket");
            dbEntry.TicketName.ShouldBeEqualTo("opera aida");
            dbEntry.Type.ShouldBeEqualTo((int)TicketActionType.Add);
            dbEntry.Description.ShouldBeEqualTo("TicketDescription");
            dbEntry.EventDate.ShouldBeEqualTo(DateTimeNow);
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public async Task ShouldGetFilteredEntries(string userId, string ticketId, string ticketName, TicketActionType type, string description, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            // Arrange
            Context.TicketActionLogEntries.AddRange(TicketActionLogEntryFaker.Generate(10));

            Context.TicketActionLogEntries.Add(new TicketActionLogEntryDbModel
            {
                UserId = "alexpvt",
                TicketId = "ticket",
                TicketName = "opera aida",
                Type = (int)TicketActionType.Add,
                Description = "TicketDescription",
                EventDate = DateTimeNow
            });

            await Context.SaveChangesAsync();

            var filter = new TicketLogsFilter
            {
                UserId = userId,
                TicketId = ticketId,
                TicketName = ticketName,
                Description = description,
                ActionType = type,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            // Act
            var entries = await _ticketLogService.GetLogsAsync(filter);

            // Assert
            entries.Count().ShouldBeEqualTo(1);

            var entry = entries.Single();

            entry.UserId.ShouldBeEqualTo("alexpvt");
            entry.TicketId.ShouldBeEqualTo("ticket");
            entry.TicketName.ShouldBeEqualTo("opera aida");
            entry.ActionType.ShouldBeEqualTo(TicketActionType.Add);
            entry.Description.ShouldBeEqualTo("TicketDescription");
            entry.EventDate.ShouldBeEqualTo(DateTimeNow);
        }

        public static IEnumerable<object[]> Filters =>
            new List<object[]>
            {
                new object[] { "expvt", string.Empty, string.Empty, TicketActionType.Unknown, string.Empty },
                new object[] { string.Empty, "icket", string.Empty, TicketActionType.Unknown, string.Empty },
                new object[] { string.Empty, string.Empty, "aida", TicketActionType.Unknown, string.Empty },
                new object[] { string.Empty, string.Empty, string.Empty, TicketActionType.Gaze | TicketActionType.Add, string.Empty},
                new object[] { string.Empty, string.Empty, string.Empty, TicketActionType.Unknown, "description"},
                new object[] { string.Empty, string.Empty, string.Empty, TicketActionType.Unknown, string.Empty, DateFrom, DateTo},
                new object[] { "expvt", "icket", "aida", TicketActionType.Add, "description", DateFrom, DateTo }
            };
    }
}
