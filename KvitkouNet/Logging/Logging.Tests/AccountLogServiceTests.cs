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
    public class AccountLogServiceTests : TestBase
    {
        private readonly IAccountLogService _accountLogService;

        public AccountLogServiceTests()
        {
            _accountLogService = new AccountLogService(context: Context, mapper: Mapper);
        }

        [Fact]
        public async Task ShouldAddLogEntry()
        {
            // Arrange
            var entry = new AccountLogEntry
            {
                UserId = "alexpvt",
                UserName = "Alexander Pashnikov",
                Email = "alexpvt@yandex.ru",
                Type = AccountActionType.SignIn,
                EventDate = DateTimeNow
            };

            // Act
            await _accountLogService.AddLogAsync(entry);

            // Assert
            var dbEntry = Context.AccountLogEntries.Single();
            
            dbEntry.Id.ShouldNotBeNullOrEmpty();
            dbEntry.UserId.ShouldBeEqualTo("alexpvt");
            dbEntry.UserName.ShouldBeEqualTo("Alexander Pashnikov");
            dbEntry.Email.ShouldBeEqualTo("alexpvt@yandex.ru");
            dbEntry.Type.ShouldBeEqualTo((int)AccountActionType.SignIn);
            dbEntry.EventDate.ShouldBeEqualTo(DateTimeNow);
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public async Task ShouldGetFilteredEntries(string userId, string userName, string email, AccountActionType type, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            // Arrange
            Context.AccountLogEntries.AddRange(AccountLogEntryFaker.Generate(10));

            Context.AccountLogEntries.Add(new AccountLogEntryDbModel
            {
                UserId = "alexpvt",
                UserName = "Alexander Pashnikov",
                Email = "alexpvt@yandex.ru",
                Type = (int) AccountActionType.SignIn,
                EventDate = DateTimeNow
            });

            await Context.SaveChangesAsync();

            var filter = new AccountLogsFilter
            {
                UserId = userId,
                UserName = userName,
                Email = email,
                Type = type,
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            
            // Act
            var entries = await _accountLogService.GetLogsAsync(filter);

            // Assert
            entries.Count().ShouldBeEqualTo(1);

            var entry = entries.Single();

            entry.UserId.ShouldBeEqualTo("alexpvt");
            entry.UserName.ShouldBeEqualTo("Alexander Pashnikov");
            entry.Email.ShouldBeEqualTo("alexpvt@yandex.ru");
            entry.Type.ShouldBeEqualTo(AccountActionType.SignIn);
            entry.EventDate.ShouldBeEqualTo(DateTimeNow);
        }

        public static IEnumerable<object[]> Filters =>
            new List<object[]>
            {
                new object[] { "expvt", string.Empty, string.Empty, AccountActionType.Unknown },
                new object[] { string.Empty, "pashnikov", string.Empty,  AccountActionType.Unknown },
                new object[] { string.Empty, string.Empty, "yandex",  AccountActionType.Unknown },
                new object[] { string.Empty, string.Empty, string.Empty,  AccountActionType.SignIn | AccountActionType.LogOut},
                new object[] { string.Empty, string.Empty, string.Empty,  AccountActionType.Unknown, DateFrom, DateTo},
                new object[] { "expvt", "pashnikov", "yandex",  AccountActionType.SignIn, DateFrom, DateTo }
            };
    }
}
