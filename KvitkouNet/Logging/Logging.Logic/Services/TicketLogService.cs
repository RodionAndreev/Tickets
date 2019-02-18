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
    public class TicketLogService : BaseService, ITicketLogService
    {
        public TicketLogService(LoggingDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<TicketActionLogEntry>> GetLogsAsync(TicketLogsFilter filter)
        {
            var dbModels =
                await Context
                    .TicketActionLogEntries
                    .Where(ComposeFilter(filter))
                    .ToListAsync()
                    .ConfigureAwait(false);

            return Mapper.Map<IEnumerable<TicketActionLogEntry>>(dbModels);
        }

        public async Task AddLogAsync(TicketActionLogEntry entry)
        {
            var dbModel = Mapper.Map<TicketActionLogEntryDbModel>(entry);

            Context.TicketActionLogEntries.Add(dbModel);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        private Expression<Func<TicketActionLogEntryDbModel, bool>> ComposeFilter(TicketLogsFilter filter)
        {
            var exp = base.ComposeBaseFilter<TicketActionLogEntryDbModel>(filter);

            if (!string.IsNullOrWhiteSpace(filter.UserId))
                exp = PredicateExtensions.And(exp, entry => entry.UserId.ToLower().Contains(filter.UserId.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.TicketId))
                exp = PredicateExtensions.And(exp, entry => entry.TicketId.ToLower().Contains(filter.TicketId.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.TicketName))
                exp = PredicateExtensions.And(exp, entry => entry.TicketName.ToLower().Contains(filter.TicketName.ToLower()));

            if (filter.ActionType != TicketActionType.Unknown)
                exp = PredicateExtensions.And(exp, entry => filter.ActionType.HasFlag((TicketActionType)entry.Type));

            if (!string.IsNullOrWhiteSpace(filter.Description))
                exp = PredicateExtensions.And(exp, entry => entry.Description.ToLower().Contains(filter.Description.ToLower()));

            return exp;
        }
    }
}
