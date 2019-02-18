using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Data.ContextModels;

namespace Security.Data.Configuration
{
    internal class UserRightsAccessFunctionConfiguration : IEntityTypeConfiguration<UserRightsAccessFunction>
    {
        public void Configure(EntityTypeBuilder<UserRightsAccessFunction> userRightsAccessFunctionEntity)
        {
            userRightsAccessFunctionEntity.HasKey(bc => new { bc.UserId, bc.AccessFunctionId });
            userRightsAccessFunctionEntity
                .HasOne<UserRights>(bc => bc.UserRights)
                .WithMany(b => b.UserRightsAccessFunction)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            userRightsAccessFunctionEntity
                .HasOne<AccessFunction>(bc => bc.AccessFunction)
                .WithMany(b => b.UserRightsAccessFunction)
                .HasForeignKey(bc => bc.AccessFunctionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
