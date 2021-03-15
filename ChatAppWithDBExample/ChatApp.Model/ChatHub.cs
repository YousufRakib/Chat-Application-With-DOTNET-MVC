using ChatAppWithDBExample.ChatApp.BLL.UserManager;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatAppWithDBExample.ChatApp.Model
{
    public class ChatHub:Hub
    {
        public static IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

        public override Task OnConnected()
        {
            int userId = new UserManager().AddUserConnection(Guid.Parse(Context.ConnectionId));
            Clients.All.BroadcastOnlineUser(userId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            int userId = new UserManager().RemoveUserConnection(Guid.Parse(Context.ConnectionId));
            Clients.All.BroadcastOfflineUser(userId);
            return base.OnDisconnected(stopCalled);
        }

        public void GetUsersToChat()
        {
            int UserId = int.Parse(HttpContext.Current.User.Identity.Name);
            List<UserDTO> users = new UserManager().GetUsersToChat();
            Clients.Clients(new UserManager().GetUSerConnections(UserId)).BroadcastUsersToChat(users);
        }

        public static void OfflineUser(int UserId)
        {
            context.Clients.All.BroadcastOfflineUser(UserId);
        }

        public static void RecieveMessage(int fromUserId, int toUserId, string message)
        {
            context.Clients.Clients(new UserManager().GetUSerConnections(toUserId)).BroadcastRecieveMessage(fromUserId, message);
        }
    }
}