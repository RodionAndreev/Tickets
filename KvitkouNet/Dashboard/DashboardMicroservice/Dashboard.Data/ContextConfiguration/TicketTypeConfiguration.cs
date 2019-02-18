using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dashboard.Data.DbModels;

namespace Dashboard.Data.ContextConfiguration
{
    internal class TicketTypeConfiguration : IEntityTypeConfiguration<TicketInfoDb>
    {
        public void Configure(EntityTypeBuilder<TicketInfoDb> builder)
        {
            builder
                .ToTable("Ticket")
                .HasKey(keyExpression: x => x.TicketId);

        }
    }
}
