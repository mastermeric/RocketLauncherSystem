using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace RealtimePushServer.Hubs
{
    public class NotificationHub:Hub
    {

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            Console.WriteLine("message ==>" + message);
        }

        public async Task SendMessageToAll(string gonderen, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", gonderen, message);
            Console.WriteLine("message ==>" + gonderen + " -> " + message);
        }
        
        public override async Task OnConnectedAsync()
        {
            //Set value in Session object.
            //var httpContext = Context.GetHttpContext();
            //httpContext.Session.SetString("Person", "Mudassar");

            await Clients.Caller.SendAsync("ReceiveMessageFromServerHub", "Server says :" + this.Context.ConnectionId + " is conencted now.\n");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("ReceiveMessageFromServerHub", "Server says :" + this.Context.ConnectionId + " is disconencted !\n");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
