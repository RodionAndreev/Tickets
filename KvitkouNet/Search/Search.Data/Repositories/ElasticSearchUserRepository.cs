using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using Search.Logic.Common.Models;

namespace Search.Data.Repositories
{
    public class ElasticSearchUserRepository : ElasticSearchRepository<UserInfo>, IUserRepository
    {
        private readonly IElasticClient _client;

        public ElasticSearchUserRepository(IElasticClient client) : base(client)
        {
            _client = client;
        }

        public async Task<SearchResult<UserInfo>> SearchAsync(UserSearchRequest request)
        {
            var result = await _client.SearchAsync<UserInfo>(descriptor => descriptor
                 .Index(nameof(UserInfo).ToLowerInvariant())
                 .Type(nameof(UserInfo))
                 .Query(BuildQuery)
                 .From(request.Offset)
                 .Size(request.Limit));

            return new SearchResult<UserInfo>
            {
                Items = result.Documents,
                Total = (int)result.Total
            };

            QueryContainer BuildQuery(QueryContainerDescriptor<UserInfo> q)
            {
                var queries = new List<QueryContainer>();
                if (request.MinRating != null)
                {
                    queries.Add(q.Range(d => d
                        .Field(t => t.Rating)
                        .GreaterThanOrEquals((double)request.MinRating.Value)));
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
