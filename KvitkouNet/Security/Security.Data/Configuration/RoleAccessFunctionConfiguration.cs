using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Data.ContextModels;

namespace Security.Data.Configuration
{
    internal class RoleAccessFunctionConfiguration : IEntityTypeConfiguration<RoleAccessFunction>
    {
        public void Configure(EntityTypeBuilder<RoleAccessFunction> roleAccessFunctionEntity)
        {
            roleAccessFunctionEntity.HasKey(bc => new { bc.RoleId, bc.AccessFunctionId });
            roleAccessFunctionEntity.HasIndex(l => l.RoleId);
            roleAccessFunctionEntity
                .HasOne<Role>(bc => bc.Role)
                .WithMany(b => b.RoleAccessFunction)
                .HasForeignKey(bc => bc.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            roleAccessFunctionEntity
                .HasOne<AccessFunction>(bc => bc.AccessFunction)
                .WithMany(l=>l.RoleAccessFunction)
                .HasForeignKey(bc => bc.AccessFunctionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
