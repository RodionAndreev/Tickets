using Microsoft.EntityFrameworkCore;
using Search.Data.Models;

namespace Search.Data.Context
{
    /// <summary>
    /// Database context for search entities.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class SearchContext : DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options)
            : base(options)
        { }

        public DbSet<SearchEntity> SearchEntities { get; set; }

        public DbSet<TicketSearchEntity> TicketSearchEntities { get; set; }

        public DbSet<UserSearchEntity> UserSearchEntities { get; set; }
    }
}
