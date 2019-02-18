using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Data.ContextModels;

namespace Security.Data.Configuration
{
    internal class UserRightsAccessRightConfiguration : IEntityTypeConfiguration<UserRightsAccessRight>
    {
        public void Configure(EntityTypeBuilder<UserRightsAccessRight> userRightsAccessRightEntity)
        {
            userRightsAccessRightEntity.HasKey(bc => new { bc.UserId, bc.AccessRightId });
            userRightsAccessRightEntity
                .HasOne<UserRights>(bc => bc.UserRights)
                .WithMany(b => b.UserRightsAccessRight)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            userRightsAccessRightEntity
                .HasOne<AccessRight>(bc => bc.AccessRight)
                .WithMany(b => b.UserRightsAccessRight)
                .HasForeignKey(bc => bc.AccessRightId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
