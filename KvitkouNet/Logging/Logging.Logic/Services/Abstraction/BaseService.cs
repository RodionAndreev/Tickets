using System;
using System.Linq.Expressions;
using AutoMapper;
using Logging.Data;
using Logging.Data.DbModels.Abstraction;
using Logging.Logic.Extensions;
using Logging.Logic.Models.Filters.Abstraction;

namespace Logging.Logic.Services.Abstraction
{
    public class BaseService : IDisposable
    {
        private bool _disposed;

        public BaseService(LoggingDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        protected LoggingDbContext Context { get; }

        protected IMapper Mapper { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Context?.Dispose();
            }

            _disposed = true;
        }

        protected Expression<Func<TModel, bool>> ComposeBaseFilter<TModel>(BaseLogFilter filter) where TModel : BaseLogEntryDbModel
        {
            var exp = PredicateExtensions.Begin<TModel>();

            if(filter.DateFrom.HasValue)
                exp = PredicateExtensions.And<TModel>(exp, error => error.EventDate >= filter.DateFrom);

            if (filter.DateTo.HasValue)
                exp = PredicateExtensions.And<TModel>(exp, error => error.EventDate <= filter.DateTo);

            return exp;
        }
    }
}
