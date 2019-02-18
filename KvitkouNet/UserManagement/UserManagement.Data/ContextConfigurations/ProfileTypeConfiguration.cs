using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.ContextConfigurations
{
    internal class ProfileTypeConfiguration : IEntityTypeConfiguration<ProfileDB>
    {
        public void Configure(EntityTypeBuilder<ProfileDB> builder)
        {
            builder.ToTable("Profiles")
                .HasKey(keyExpression: x => x.Id);
            builder.HasOne(navigationExpression: x => x.UserDB)
                .WithOne(u => u.ProfileDB)
                .HasForeignKey<ProfileDB>(x => x.UserDBId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}