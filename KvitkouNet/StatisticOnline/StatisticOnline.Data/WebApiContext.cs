using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StatisticOnline.Data.Models;

namespace StatisticOnline.Data.Context
{
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {

        }

        public DbSet<OnlineDb> StatisticOnline { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookTypeConfiguration());
        }

        public class BookTypeConfiguration : IEntityTypeConfiguration<OnlineDb>
        {
            public void Configure(EntityTypeBuilder<OnlineDb> builder)
            {
                builder.ToTable("StatisticOnline")
                    .HasKey(x => x.Id);

                builder.Property(x => x.CountAll)
                    .IsRequired();

                builder.Property(x => x.CountGuest)
                    .IsRequired();

                builder.Property(x => x.CountRegistered)
                    .IsRequired();

                builder.Property(x => x.CreateTime)
                    .IsRequired()
                    .HasDefaultValue(new DateTime());
            }
        }

    }
}
