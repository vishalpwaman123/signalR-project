using Microsoft.AspNetCore.SignalR;

namespace SignalR.Web.Application
{
    public class ShoppingListHub : Hub
    {
        //By Default Method
        /*public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("Method1", $"{Context.ConnectionId} has joined");
            await Clients.All.SendAsync("Method1", $"{Context.ConnectionId} has joined");
            await Clients.Caller.SendAsync("Method1", $"{Context.ConnectionId} has joined");
            await Clients.Group("Group_Name").SendAsync("Method1", $"{Context.ConnectionId} jas joined");
        }*/

        //By Default Method
        /*public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }*/
    }
}
