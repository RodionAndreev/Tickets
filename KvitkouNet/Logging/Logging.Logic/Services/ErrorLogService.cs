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
    public class ErrorLogService : BaseService, IErrorLogService
    {
        public ErrorLogService(LoggingDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<InternalErrorLogEntry>> GetLogsAsync(ErrorLogsFilter filter)
        {
            var dbModels = 
                await Context
                    .InternalErrorLogEntries
                    .Where(ComposeFilter(filter))
                    .ToListAsync()
                    .ConfigureAwait(false);

            return Mapper.Map<IEnumerable<InternalErrorLogEntry>>(dbModels);
        }

        public async Task AddLogAsync(InternalErrorLogEntry entry)
        {
            var dbModel = Mapper.Map<InternalErrorLogEntryDbModel>(entry);

            Context.InternalErrorLogEntries.Add(dbModel);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
        
        private Expression<Func<InternalErrorLogEntryDbModel, bool>> ComposeFilter(ErrorLogsFilter filter)
        {
            var exp = base.ComposeBaseFilter<InternalErrorLogEntryDbModel>(filter);

            if(!string.IsNullOrWhiteSpace(filter.ServiceName))
                exp = PredicateExtensions.And(exp, entry => entry.ServiceName.ToLower().Contains(filter.ServiceName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.ExceptionTypeName))
                exp = PredicateExtensions.And(exp, entry => entry.ExceptionType.ToLower().Contains(filter.ExceptionTypeName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Message))
                exp = PredicateExtensions.And(exp, entry => entry.Message.ToLower().Contains(filter.Message.ToLower()));

            return exp;
        }
    }
}
