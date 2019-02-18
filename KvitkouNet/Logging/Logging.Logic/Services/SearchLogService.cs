using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Logging.Data;
using Logging.Data.DbModels;
using Logging.Logic.Extensions;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;
using Logging.Logic.Models.Filters;
using Logging.Logic.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Logging.Logic.Services
{
    public class SearchLogService : BaseService, ISearchLogService
    {
        public SearchLogService(LoggingDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<SearchQueryLogEntry>> GetLogsAsync(SearchQueryLogsFilter filter)
        {
            var dbModels =
                await Context
                    .SearchQueryLogEntries
                    .Where(ComposeFilter(filter))
                    .ToListAsync()
                    .ConfigureAwait(false);

            return Mapper.Map<IEnumerable<SearchQueryLogEntry>>(dbModels);
        }

        public async Task AddLogAsync(SearchQueryLogEntry entry)
        {
            var dbModel = Mapper.Map<SearchQueryLogEntryDbModel>(entry);

            Context.SearchQueryLogEntries.Add(dbModel);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        private Expression<Func<SearchQueryLogEntryDbModel, bool>> ComposeFilter(SearchQueryLogsFilter filter)
        {
            var exp = base.ComposeBaseFilter<SearchQueryLogEntryDbModel>(filter);

            if (!string.IsNullOrWhiteSpace(filter.UserId))
                exp = PredicateExtensions.And(exp, entry => entry.UserId.ToLower().Contains(filter.UserId.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.SearchCriterium))
                exp = PredicateExtensions.And(exp, entry => entry.SearchCriterium.ToLower().Contains(filter.SearchCriterium.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.FilterInfo))
                exp = PredicateExtensions.And(exp, entry => entry.FilterInfo.ToLower().Contains(filter.FilterInfo.ToLower()));

            return exp;
        }
    }
}
