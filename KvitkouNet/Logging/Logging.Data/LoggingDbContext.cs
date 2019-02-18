using Logging.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Logging.Data
{
	public sealed class LoggingDbContext : DbContext
	{
		public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
		{
		}

		public DbSet<AccountLogEntryDbModel> AccountLogEntries { get; set; }

		public DbSet<InternalErrorLogEntryDbModel> InternalErrorLogEntries { get; set; }

		public DbSet<PaymentLogEntryDbModel> PaymentLogEntries { get; set; }

		public DbSet<SearchQueryLogEntryDbModel> SearchQueryLogEntries { get; set; }

		public DbSet<TicketActionLogEntryDbModel> TicketActionLogEntries { get; set; }

		public DbSet<TicketDealLogEntryDbModel> TicketDealLogEntries { get; set; }
	}
}
