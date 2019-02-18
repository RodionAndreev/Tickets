using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssert;
using Logging.Data.DbModels;
using Logging.Data.Fakers;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;
using Logging.Logic.Models.Filters;
using Logging.Logic.Services;
using Xunit;

namespace Logging.Tests
{
    public class SearchLogServiceTests : TestBase
    {
        private readonly ISearchLogService _searchLogService;

        public SearchLogServiceTests()
        {
            _searchLogService = new SearchLogService(context: Context, mapper: Mapper);
        }

        [Fact]
        public async Task ShouldAddLogEntry()
        {
            // Arrange
            var entry = new SearchQueryLogEntry
            {
                UserId = "alexpvt",
                SearchCriterium = "opera aida",
                FilterInfo = "theaters, opera",
                EventDate = DateTimeNow
            };

            // Act
            await _searchLogService.AddLogAsync(entry);

            // Assert
            var dbEntry = Context.SearchQueryLogEntries.Single();

            dbEntry.Id.ShouldNotBeNullOrEmpty();
            dbEntry.UserId.ShouldBeEqualTo("alexpvt");
            dbEntry.SearchCriterium.ShouldBeEqualTo("opera aida");
            dbEntry.FilterInfo.ShouldBeEqualTo("theaters, opera");
            dbEntry.EventDate.ShouldBeEqualTo(DateTimeNow);
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public async Task ShouldGetFilteredEntries(string userId, string userName, string filterInfo, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            // Arrange
            Context.SearchQueryLogEntries.AddRange(SearchQueryLogEntryFaker.Generate(10));

            Context.SearchQueryLogEntries.Add(new SearchQueryLogEntryDbModel
            {
                UserId = "alexpvt",
                SearchCriterium = "opera aida",
                FilterInfo = "theaters, opera",
                EventDate = DateTimeNow
            });

            await Context.SaveChangesAsync();

            var filter = new SearchQueryLogsFilter()
            {
                UserId = userId,
                SearchCriterium = userName,
                FilterInfo = filterInfo,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            // Act
            var entries = await _searchLogService.GetLogsAsync(filter);

            // Assert
            entries.Count().ShouldBeEqualTo(1);

            var entry = entries.Single();

            entry.UserId.ShouldBeEqualTo("alexpvt");
            entry.SearchCriterium.ShouldBeEqualTo("opera aida");
            entry.FilterInfo.ShouldBeEqualTo("theaters, opera");
            entry.EventDate.ShouldBeEqualTo(DateTimeNow);
        }

        public static IEnumerable<object[]> Filters =>
            new List<object[]>
            {
                new object[] { "expvt", string.Empty, string.Empty },
                new object[] { string.Empty, "opera", string.Empty },
                new object[] { string.Empty, string.Empty, "theaters" },
                new object[] { string.Empty, string.Empty, string.Empty, DateFrom, DateTo},
                new object[] { "expvt", "opera", "theaters", DateFrom, DateTo }
            };
    }
}
