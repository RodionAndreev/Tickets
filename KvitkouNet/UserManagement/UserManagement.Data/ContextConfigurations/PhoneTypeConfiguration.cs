using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.ContextConfigurations
{
    internal class PhoneTypeConfiguration : IEntityTypeConfiguration<PhoneNumberDB>
    {
        public void Configure(EntityTypeBuilder<PhoneNumberDB> builder)
        {
            builder.ToTable("PhoneNumbers")
                .HasKey(keyExpression: x => x.Id);
        }
    }
}