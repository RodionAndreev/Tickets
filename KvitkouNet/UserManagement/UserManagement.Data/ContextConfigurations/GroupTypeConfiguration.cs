using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.ContextConfigurations
{
    internal class GroupTypeConfiguration : IEntityTypeConfiguration<GroupDB>
    {
        public void Configure(EntityTypeBuilder<GroupDB> builder)
        {
            builder.ToTable("Groups")
                .HasKey(keyExpression: x => x.Id);

        }
    }
}