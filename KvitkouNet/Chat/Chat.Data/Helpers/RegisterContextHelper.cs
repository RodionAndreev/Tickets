using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Chat.Data.Context;
using Chat.Data.DbModels;
using Chat.Data.Fakers;


namespace Chat.Data.Helpers
{
    public class RegisterContextHelper
    {
        private string m_dataSource = @"Data Source=./ChatDB.db";

        public RegisterContextHelper()
        {
            var o = new DbContextOptionsBuilder<ChatContext>();
            o.UseSqlite(m_dataSource);

            using (var ctx = new ChatContext(o.Options))
            {
                //если нету DB - создадим
                ctx.Database.EnsureCreated();

                //создадим тестового абонента
                if (!ctx.Rooms.Any())
                {
                    //создадим комнату номер 1. Владелец комнаты тестовый пользователь UserId = 1
                    ctx.Rooms.AddAsync(new RoomDb()
                    {
                        Id = "1",
                        OwnerId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                        Name = "тестовая комната номер 1",
                        UpdateDate = DateTime.Now,
                        IsPrivat = false                     
                    });

                    // тестовый UserId = 1. Он владелц комнаты номер 1
                    ctx.Messages.AddAsync(new MessageDb()
                    {
                        Id = "1",
                        RoomId = "1",
                        Text = "тестовое сообщение номер 1",
                        UserId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                        UpdateDate = DateTime.Now,
                        IsEdit = false,
                        SendedTime = DateTime.Now
                    });

                    //создадим натсройки для тестового пользователя UserId = 1 
                    ctx.Settings.AddAsync(new SettingsDb()
                    {
                        Id = "1",
                        UserId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                        UpdateDate = DateTime.Now,
                        DisablePrivateMessages = false,
                        ViewTimestampsMessage = false,
                        HistoryCountsMessages = 15,
                        BackgroundColor = BackgroundColorType.Black,
                        Tab = false,
                        Sound = false,
                        HideChat = false,
                        Toast = false
                    });

                    //создадим тестового пользователя номер 1. Он будет владельцем комнаты номер 1 и ему пренадлежит сообщение номер 1.
                    ctx.Users.AddAsync(new UserDb()
                    {
                        Id = "5BE86359-073C-434B-AD2D-A3932222DABE",
                        IsOnline = false,
                        RoomId = "1",
                        SettingsId = "1",
                        UpdateDate = DateTime.Now,
                        UserName = "Тестовый пользователь номер 1"
                    });
                    ctx.SaveChanges();
                }

            }
        }

        public Action<DbContextOptionsBuilder> GetOptionsBuilder()
        {
            return opt => opt.UseSqlite(connectionString: m_dataSource);
        }
    }
}
