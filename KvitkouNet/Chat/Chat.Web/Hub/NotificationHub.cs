using System.Linq;
using System.Threading.Tasks;
using Chat.Logic.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Web.Hub
{
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task InAlertOnComingMessageAllUsers(Message message)
        {
            await this.Clients.All.SendAsync("alertOnComingMessageAllUsers", message);
        }

        //test
        public async Task NotifyUsers(string[] ids)
        {
            await this.Clients.Users(ids.ToList().AsReadOnly()).SendAsync("getAlert", "asdasdasd");
        }
    }
}

