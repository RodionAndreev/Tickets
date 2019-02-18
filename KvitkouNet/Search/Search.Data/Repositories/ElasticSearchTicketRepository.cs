using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using Search.Logic.Common.Models;

namespace Search.Data.Repositories
{
    public class ElasticSearchTicketRepository : ElasticSearchRepository<TicketInfo>, ITicketRepository

    {
        private readonly IElasticClient _client;

        public ElasticSearchTicketRepository(IElasticClient client) : base(client)
        {
            _client = client;
        }

        public async Task<SearchResult<TicketInfo>> SearchAsync(TicketSearchRequest request)
        {
            var result = await _client.SearchAsync<TicketInfo>(descriptor => descriptor
                 .Index(nameof(TicketInfo).ToLowerInvariant())
                 .Type(nameof(TicketInfo))
                 .Query(BuildQuery)
                 .From(request.Offset)
                 .Size(request.Limit));

            return new SearchResult<TicketInfo>
            {
                Items = result.Documents,
                Total = (int)result.Total
            };

            QueryContainer BuildQuery(QueryContainerDescriptor<TicketInfo> q)
            {
                var queries = new List<QueryContainer>();
                if (request.DateFrom != null)
                {
                    queries.Add(q.DateRange(d => d
                        .Field(t => t.Date)
                        .GreaterThanOrEquals(request.DateFrom.Value)));
                }
                if (request.DateTo != null)
                {
                    queries.Add(q.DateRange(d => d
                        .Field(t => t.Date)
                        .LessThan(request.DateTo.Value.AddDays(1))));
                }
                if (request.MinPrice != null)
                {
                    queries.Add(q.Range(d => d
                        .Field(t => t.Price)
                        .GreaterThanOrEquals((double)request.MinPrice.Value)));
                }
                if (request.MaxPrice != null)
                {
                    queries.Add(q.Range(d => d
                        .Field(t => t.Price)
                        .LessThanOrEquals((double)request.MaxPrice.Value)));
                }
                if (request.Category != null)
                {
                    queries.Add(q.Match(d => d
                        .Field(t => t.Category)
                        .Query(request.Category)));
                }
                if (request.City != null)
                {
                    queries.Add(q.Match(d => d
                        .Field(t => t.City)
                        .Query(request.City)));
                }
                if (request.Name != null)
                {
                    queries.Add(q.Fuzzy(d => d
                        .Field(t => t.Name)
                        .Value(request.Name.ToLowerInvariant())
                        .Fuzziness(Fuzziness.Auto)
                        .Transpositions()
                    ));
                }

                if (queries.Count > 0)
                {
                    return q.Bool(b => b.Must(queries.ToArray()));
                }
                return q.MatchAll();
            }
        }
    }
}
