using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Search.Data.Repositories;
using Search.Logic.Common.Fakers;
using Search.Logic.Common.Models;
using Xunit;

namespace Search.Tests
{
    public class ElasticSearchTicketRepositoryTests : IDisposable
    {
        private readonly ElasticClient _elasticClient;
        private readonly ElasticSearchTicketRepository _elasticRepository;

        public ElasticSearchTicketRepositoryTests()
        {
            _elasticClient = new ElasticClient(new ConnectionSettings(new Uri("http://localhost:9200")));
            _elasticRepository = new ElasticSearchTicketRepository(_elasticClient);
        }

        [Fact]
        public async Task Test_Insert_Data_to_ElasticSearch()
        {
            await _elasticRepository.SaveAsync(TicketInfoFaker.Generate(1).First());

            var result = await _elasticRepository.SearchAsync(new TicketSearchRequest()
            {
                Offset = 0,
                Limit = 10
            });

            result.Total.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Test_Search_Data_by_Date()
        {
            var ticket = new TicketInfo
            {
                Id = "dsbgdfhd",
                Category = "Cinema",
                Date = new DateTime(2019, 1, 20)
            };
            await _elasticRepository.SaveAsync(ticket);

            var result = await _elasticRepository.SearchAsync(new TicketSearchRequest()
            {
                Offset = 0,
                Limit = 10,
                DateFrom = new DateTime(2019, 1, 15),
                DateTo = new DateTime(2019, 1, 30)
            });

            result.Total.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Test_Search_Data_by_Date_and_Category()
        {
            var ticket = new TicketInfo
            {
                Id = "dsbgdfhd",
                Category = "Cinema",
                Date = new DateTime(2019, 1, 20)
            };
            await _elasticRepository.SaveAsync(ticket);

            var result = await _elasticRepository.SearchAsync(new TicketSearchRequest()
            {
                Offset = 0,
                Limit = 10,
                DateFrom = new DateTime(2019, 1, 15),
                Category = "Cinema"
            });

            result.Total.Should().Be(1);
        }

        [Fact]
        public async Task Test_Search_Data_by_Name()
        {
            var ticket = new TicketInfo
            {
                Id = "dsbgdfhd",
                Category = "Cinema",
                Date = new DateTime(2019, 1, 20),
                Name = "Hello"
            };
            await _elasticRepository.SaveAsync(ticket);

            var result = await _elasticRepository.SearchAsync(new TicketSearchRequest()
            {
                Offset = 0,
                Limit = 10,
                Name = "helo"
            });

            result.Total.Should().Be(1);
        }

        public void Dispose()
        {
            _elasticClient.DeleteIndex(nameof(TicketInfo).ToLowerInvariant());
        }
    }
}
