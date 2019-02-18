using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Data.ContextModels;

namespace Security.Data.Configuration
{
    internal class FeatureAccessRightConfiguration : IEntityTypeConfiguration<FeatureAccessRight>
    {
        public void Configure(EntityTypeBuilder<FeatureAccessRight> featureAccessRightEntity)
        {
            featureAccessRightEntity.HasKey(bc => new { bc.FeatureId, bc.AccessRightId });
            featureAccessRightEntity.HasIndex(l => l.FeatureId);
            featureAccessRightEntity
                .HasOne<Feature>(bc => bc.Feature)
                .WithMany(b => b.FeatureAccessRight)
                .HasForeignKey(bc => bc.FeatureId)
                .OnDelete(DeleteBehavior.Cascade);
            featureAccessRightEntity
                .HasOne<AccessRight>(bc => bc.AccessRight)
                .WithMany(l=>l.FeatureAccessRight)
                .HasForeignKey(l=>l.AccessRightId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
