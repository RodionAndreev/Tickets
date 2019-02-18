using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.ContextConfigurations
{
    internal class UserGroupTypeConfiguration : IEntityTypeConfiguration<UserGroupDB>
    {
        public void Configure(EntityTypeBuilder<UserGroupDB> builder)
        {
            builder.HasKey(x => new { x.GroupId, x.UserId });
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Group)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.GroupId);
        }
    }
}