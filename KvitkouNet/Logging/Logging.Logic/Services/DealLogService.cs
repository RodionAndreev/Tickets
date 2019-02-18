using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Logging.Data;
using Logging.Data.DbModels;
using Logging.Logic.Enums;
using Logging.Logic.Extensions;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;
using Logging.Logic.Models.Filters;
using Logging.Logic.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Logging.Logic.Services
{
    public class DealLogService : BaseService, IDealLogService
    {
        public DealLogService(LoggingDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<TicketDealLogEntry>> GetLogsAsync(DealLogFilter filter)
        {
            var dbModels =
                await Context
                    .TicketDealLogEntries
                    .Where(ComposeFilter(filter))
                    .ToListAsync()
                    .ConfigureAwait(false);

            return Mapper.Map<IEnumerable<TicketDealLogEntry>>(dbModels);
        }

        public async Task AddLogAsync(TicketDealLogEntry entry)
        {
            var dbModel = Mapper.Map<TicketDealLogEntryDbModel>(entry);

            Context.TicketDealLogEntries.Add(dbModel);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        private Expression<Func<TicketDealLogEntryDbModel, bool>> ComposeFilter(DealLogFilter filter)
        {
            var exp = base.ComposeBaseFilter<TicketDealLogEntryDbModel>(filter);

            if (!string.IsNullOrWhiteSpace(filter.TicketId))
                exp = PredicateExtensions.And(exp, entry => entry.TicketId.ToLower().Contains(filter.TicketId.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.OwnerId))
                exp = PredicateExtensions.And(exp, entry => entry.OwnerId.ToLower().Contains(filter.OwnerId.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.RecieverId))
                exp = PredicateExtensions.And(exp, entry => entry.RecieverId.ToLower().Contains(filter.RecieverId.ToLower()));

            if (filter.MinPrice.HasValue)
                exp = PredicateExtensions.And(exp, entry => entry.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                exp = PredicateExtensions.And(exp, entry => entry.Price <= filter.MaxPrice.Value);

            if (filter.Type != DealType.Unknown)
                exp = PredicateExtensions.And(exp, entry => filter.Type.HasFlag((DealType)entry.Type));

            return exp;
        }
    }
}
