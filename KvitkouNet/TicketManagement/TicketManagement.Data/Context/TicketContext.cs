using Microsoft.EntityFrameworkCore;
using TicketManagement.Data.DbModels;

namespace TicketManagement.Data.Context
{
    /// <summary>
    ///     Класс контекста для работы с данными в базе
    /// </summary>
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<SellerAddress> SellerAddresses { get; set; }

        public DbSet<LocationAddress> LocationAddresses { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationAddress>()
                .ToTable("LocationAddresses")
                .HasMany(p => p.Tickets)
                .WithOne(p => p.LocationEvent)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SellerAddress>()
                .ToTable("SellerAddresses")
                .HasMany(p => p.Tickets)
                .WithOne(p => p.SellerAdress)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.User)
                .WithMany(b => b.UserTickets)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}