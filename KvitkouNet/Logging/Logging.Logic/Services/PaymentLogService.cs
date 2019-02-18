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
    public class PaymentLogService : BaseService, IPaymentLogService
    {
        public PaymentLogService(LoggingDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<PaymentLogEntry>> GetLogsAsync(PaymentLogsFilter filter)
        {
            var dbModels =
                await Context
                    .PaymentLogEntries
                    .Where(ComposeFilter(filter))
                    .ToListAsync()
                    .ConfigureAwait(false);

            return Mapper.Map<IEnumerable<PaymentLogEntry>>(dbModels);
        }

        public async Task AddLogAsync(PaymentLogEntry entry)
        {
            var dbModel = Mapper.Map<PaymentLogEntryDbModel>(entry);

            Context.PaymentLogEntries.Add(dbModel);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        private Expression<Func<PaymentLogEntryDbModel, bool>> ComposeFilter(PaymentLogsFilter filter)
        {
            var exp = base.ComposeBaseFilter<PaymentLogEntryDbModel>(filter);

            if (!string.IsNullOrWhiteSpace(filter.RecieverId))
                exp = PredicateExtensions.And(exp, entry => entry.RecieverId.ToLower().Contains(filter.RecieverId.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.SenderId))
                exp = PredicateExtensions.And(exp, entry => entry.SenderId.ToLower().Contains(filter.SenderId.ToLower()));

            if (filter.MinTransfer.HasValue)
                exp = PredicateExtensions.And(exp, entry => entry.Transfer >= filter.MinTransfer.Value);

            if (filter.MaxTransfer.HasValue)
                exp = PredicateExtensions.And(exp, entry => entry.Transfer <= filter.MaxTransfer.Value);

            return exp;
        }
    }
}
