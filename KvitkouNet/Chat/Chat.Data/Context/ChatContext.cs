using Chat.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Context
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) 
            : base(options)
        {
        }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<SettingsDb> Settings { get; set; }
        public DbSet<RoomDb> Rooms { get; set; }
        public DbSet<MessageDb> Messages { get; set; }
    }
}
