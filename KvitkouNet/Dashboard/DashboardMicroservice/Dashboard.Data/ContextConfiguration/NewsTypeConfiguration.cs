using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dashboard.Data.DbModels;

namespace Dashboard.Data.ContextConfiguration
{
    internal class NewsTypeConfiguration : IEntityTypeConfiguration<NewsDb>
    {
        public void Configure(EntityTypeBuilder<NewsDb> builder)
        {
            builder
                .HasKey(keyExpression: x => x.NewsId);

            builder.HasOne(navigationExpression: x => x.Ticket)
                .WithOne(x => x.NewsDbs)
                .HasForeignKey<TicketInfoDb>(x => x.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
