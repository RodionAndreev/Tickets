using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Search.Logic.Common.Models;
using SearchRequest = Search.Logic.Common.Models.SearchRequest;

namespace Search.Data.Repositories
{
    public class ElasticSearchRepository<T> : IRepository<T> where T : EntityInfo
    {
        private readonly IElasticClient _client;

        public ElasticSearchRepository(IElasticClient client)
        {
            _client = client;
        }

        public async Task SaveAsync(T item)
        {
            await _client.IndexAsync(item, descriptor => descriptor
                .Index(typeof(T).Name.ToLowerInvariant())
                .Id(item.Id)
                .Type(typeof(T).Name)
                .Refresh(Refresh.True));
        }

        public Task DeleteAsync(string id)
        {
            return _client.DeleteAsync<T>(id, descriptor => descriptor
                .Index(typeof(T).Name.ToLowerInvariant())
                .Type(typeof(T).Name));
        }
    }
}
