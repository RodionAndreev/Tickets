using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Data.ContextModels;

namespace Security.Data.Configuration
{
    internal class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> featureEntity)
        {
            featureEntity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
