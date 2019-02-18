using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Data.ContextModels;

namespace Security.Data.Configuration
{
    internal class AccessRightConfiguration : IEntityTypeConfiguration<AccessRight>
    {
        public void Configure(EntityTypeBuilder<AccessRight> accessRightEntity)
        {
            accessRightEntity.HasKey(x => x.Id);
            accessRightEntity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
