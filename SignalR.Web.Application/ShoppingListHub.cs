using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SignalR.Web.Application
{
    public class ShoppingListHub : Hub
    {

        private static readonly ConcurrentDictionary<string, List<string>> _groupIdShoppingLists = new();

        public async Task CreateShoppingList()
        {
            string shoppingListId  = Guid.NewGuid().ToString();
            _groupIdShoppingLists.TryAdd(shoppingListId, new List<string>());
            await Groups.AddToGroupAsync(Context.ConnectionId, shoppingListId);
            await Clients.Caller.SendAsync("ShoppingListCreated", shoppingListId);
        }

        public async Task AddItem(string shoppingListId, string itemName)
        {
            if(_groupIdShoppingLists.TryGetValue(shoppingListId, out var list))
            {
                list.Add(itemName);
                await Clients.Group(shoppingListId).SendAsync("ReceiveShoppingList", list);
            }
        }

        public async Task RemoveItem(string shoppingListId, string itemName)
        {
            if (_groupIdShoppingLists.TryGetValue(shoppingListId, out var list))
            {
                list.Remove(itemName);
                await Clients.Group(shoppingListId).SendAsync("ReceiveShoppingList", list);
            }
        }

        public async Task JoinShoppingList(string shoppingListId)
        {
            if(_groupIdShoppingLists.TryGetValue(shoppingListId, out var list))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, shoppingListId);
                await Clients.Caller.SendAsync("joinShoppingList", shoppingListId, list);
            }
        }

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
