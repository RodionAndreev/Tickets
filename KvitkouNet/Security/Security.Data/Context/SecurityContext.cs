using Microsoft.EntityFrameworkCore;
using Security.Data.Configuration;
using Security.Data.ContextModels;

namespace Security.Data.Context
{
    //Контекст для Security
    public class SecurityContext : DbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Права доступа
        /// </summary>
        public DbSet<AccessRight> AccessRights { get; set; }

        /// <summary>
        /// Фичи
        /// </summary>
        public DbSet<Feature> Features { get; set; }

        /// <summary>
        /// Функции доступа к фиче
        /// </summary>
        public DbSet<AccessFunction> AccessFunctions { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Права пользователей
        /// </summary>
        public DbSet<UserRights> UsersRights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccessRightConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureAccessRightConfiguration());
            modelBuilder.ApplyConfiguration(new AccessFunctionConfiguration());
            modelBuilder.ApplyConfiguration(new AccessFunctionAccessRightConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleAccessRightConfiguration());
            modelBuilder.ApplyConfiguration(new RoleAccessFunctionConfiguration());
            modelBuilder.ApplyConfiguration(new UserRightsConfiguration());
            modelBuilder.ApplyConfiguration(new UserRightsAccessFunctionConfiguration());
            modelBuilder.ApplyConfiguration(new UserRightsAccessRightConfiguration());
            modelBuilder.ApplyConfiguration(new UserRightsRoleConfiguration());
        }
    }
}
