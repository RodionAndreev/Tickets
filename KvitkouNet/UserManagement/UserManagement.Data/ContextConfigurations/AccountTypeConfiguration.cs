using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.ContextConfigurations
{
    internal class AccountTypeConfiguration : IEntityTypeConfiguration<AccountDB>
    {
        public void Configure(EntityTypeBuilder<AccountDB> builder)
        {
            builder.ToTable("Accounts")
                .HasKey(keyExpression: x => x.Id);
            builder.HasOne(navigationExpression: x => x.UserDB)
                .WithOne(u => u.AccountDB)
                .HasForeignKey<AccountDB>(x => x.UserDBId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}