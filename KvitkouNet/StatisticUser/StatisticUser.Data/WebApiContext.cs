using Microsoft.EntityFrameworkCore;
using StatisticUser.Data.DbModels;

namespace StatisticUser.Data
{
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {

        }

        public DbSet<MessagesUsersOnSiteDB> MessagesUsersOnSite { get; set; }
        public DbSet<OpenResourcesDb> OpenResources { get; set; }
        public DbSet<RatingDB> Rating { get; set; }
        public DbSet<ResourcesUrlDB> ResourcesUrl { get; set; }
        public DbSet<TimeOnSiteDB> TimeOnSite { get; set; }
        public DbSet<SummaryTableDB> SummaryTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MessagesUsersOnSiteDB>().ToTable("MessagesUsersOnSiteDB");
            modelBuilder.Entity<OpenResourcesDb>().ToTable("OpenResourcesDb");
            modelBuilder.Entity<RatingDB>().ToTable("RatingDB");
            modelBuilder.Entity<ResourcesUrlDB>().ToTable("ResourcesUrlDB");
            modelBuilder.Entity<TimeOnSiteDB>().ToTable("TimeOnSiteDB");
            modelBuilder.Entity<SummaryTableDB>().ToTable("SummaryTableDB");
        }
    }
}
