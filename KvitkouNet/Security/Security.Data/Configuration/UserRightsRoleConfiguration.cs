using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Data.ContextModels;

namespace Security.Data.Configuration
{
    internal class UserRightsRoleConfiguration : IEntityTypeConfiguration<UserRightsRole>
    {
        public void Configure(EntityTypeBuilder<UserRightsRole> userRightsRoleEntity)
        {
            userRightsRoleEntity.HasKey(bc => new { bc.UserId, bc.RoleId });
            userRightsRoleEntity.HasIndex(l => l.UserId);
            userRightsRoleEntity
                .HasOne<UserRights>(bc => bc.UserRights)
                .WithMany(b => b.UserRightsRole)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            userRightsRoleEntity
                .HasOne<Role>(bc => bc.Role)
                .WithMany(b => b.UserRightsRole)
                .HasForeignKey(bc => bc.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
