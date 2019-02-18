using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.ContextConfigurations
{
    internal class AddressTypeConfiguration : IEntityTypeConfiguration<AddressDB>
    {
        public void Configure(EntityTypeBuilder<AddressDB> builder)
        {
            builder.ToTable("Addresses")
                .HasKey(keyExpression: x => x.Id);
        }
    }
}