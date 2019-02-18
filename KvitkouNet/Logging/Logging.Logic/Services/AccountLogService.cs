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
    public class AccountLogService : BaseService, IAccountLogService
    {
        public AccountLogService(LoggingDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<AccountLogEntry>> GetLogsAsync(AccountLogsFilter filter)
        {
            var dbModels =
                await Context
                    .AccountLogEntries
                    .Where(ComposeFilter(filter))
                    .ToListAsync()
                    .ConfigureAwait(false);

            return Mapper.Map<IEnumerable<AccountLogEntry>>(dbModels);
        }

        public async Task AddLogAsync(AccountLogEntry entry)
        {
            var dbModel = Mapper.Map<AccountLogEntryDbModel>(entry);

            Context.AccountLogEntries.Add(dbModel);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        private Expression<Func<AccountLogEntryDbModel, bool>> ComposeFilter(AccountLogsFilter filter)
        {
            var exp = base.ComposeBaseFilter<AccountLogEntryDbModel>(filter);

            if (!string.IsNullOrWhiteSpace(filter.UserId))
                exp = PredicateExtensions.And(exp, entry => entry.UserId.ToLower().Contains(filter.UserId.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.UserName))
                exp = PredicateExtensions.And(exp, entry => entry.UserName.ToLower().Contains(filter.UserName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                exp = PredicateExtensions.And(exp, entry => entry.Email.ToLower().Contains(filter.Email.ToLower()));

            if (filter.Type != AccountActionType.Unknown)
                exp = PredicateExtensions.And(exp, entry => filter.Type.HasFlag((AccountActionType)entry.Type));

            return exp;
        }
    }
}
