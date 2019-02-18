using System.Threading.Tasks;

namespace Notification.Logic.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(string userId, string name, string email);
    }
}
