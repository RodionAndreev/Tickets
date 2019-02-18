using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.ContextConfigurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserDB>
    {
        public void Configure(EntityTypeBuilder<UserDB> builder)
        {
            builder//.ToTable("Users")
                .HasKey(keyExpression: x => x.Id);
            builder.HasOne(navigationExpression: x => x.AccountDB)
                .WithOne(u=>u.UserDB)
                .HasForeignKey<AccountDB>(x => x.UserDBId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(navigationExpression: x => x.ProfileDB)
                .WithOne(u=>u.UserDB)
                .HasForeignKey<ProfileDB>(x => x.UserDBId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}