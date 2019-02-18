using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Security.Data.Context
{
    internal class SecurityContextFactory : IDesignTimeDbContextFactory<SecurityContext>
    {
        public SecurityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SecurityContext>();
            optionsBuilder.UseSqlite("Data Source=./SecurityDatabase.db");

            return new SecurityContext(optionsBuilder.Options);
        }
    }
}
