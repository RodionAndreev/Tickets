using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Data.ContextModels;

namespace Security.Data.Configuration
{
    internal class UserRightsConfiguration : IEntityTypeConfiguration<UserRights>
    {
        public void Configure(EntityTypeBuilder<UserRights> userRightEntity)
        {
            userRightEntity.HasKey(x => x.UserId);
            userRightEntity.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            userRightEntity.Property(x => x.MiddleName)
                .HasMaxLength(100);
            userRightEntity.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);
            userRightEntity.Property(x => x.UserLogin)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
