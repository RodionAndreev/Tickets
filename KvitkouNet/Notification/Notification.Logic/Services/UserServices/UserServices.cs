using Notification.Data.Context;
using Notification.Data.Models;
using Notification.Logic.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Logic.Services.UserServices
{
    public class UserServices : IUserService
    {
        private NotificationContext m_context;

        public UserServices(NotificationContext context)
        {
            m_context = context;
        }

        public async Task CreateUser(string userId, string name, string email)
        {
            if (m_context.Users.Any(x => x.Name == name))
                throw new NullReferenceException();

            await m_context.Users.AddAsync(new User
            {
                Id = userId,
                Name = name,
                Email = email
            });

            await m_context.SaveChangesAsync();
        }
    }
}
