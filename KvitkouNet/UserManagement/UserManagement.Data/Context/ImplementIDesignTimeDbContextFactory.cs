using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserManagement.Data.Context
{
    class ImplementIDesignTimeDbContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<UserContext>();

            var connectionString = "Data Source = ./UserDatabase.db";

            builder.UseLazyLoadingProxies().UseSqlite(connectionString);

            return new UserContext(builder.Options);
        }
    }
}
