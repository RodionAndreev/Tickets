using Microsoft.EntityFrameworkCore;
using Dashboard.Data.DbModels;
using Dashboard.Data.ContextConfiguration;

namespace Dashboard.Data.Context
{
    /// <summary>
    ///     Класс контекста для работы с данными в базе
    /// </summary>
    public class DashboardContext : DbContext
    {
        public DashboardContext(DbContextOptions<DashboardContext> options)
            : base(options)
        {
        }
        
        public DbSet<NewsDb> News { get; set; }
        public DbSet<TicketInfoDb> TicketInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NewsTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
