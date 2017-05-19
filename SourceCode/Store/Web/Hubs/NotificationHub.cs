using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Web.Hubs
{
    public class NotificationHub : Hub
    {
        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }

        public void Hello()
        {
            Clients.All.helloworld();
        }

        public void Send(string name, string message)
        {
            string who = Context.User.Identity.Name;
            //Clients.Group(who).addNewMessageToPage(who, message);
            //Clients.Group("tam").addNewMessageToPage(who, message);

            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Send(string name, string message, string userName)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}