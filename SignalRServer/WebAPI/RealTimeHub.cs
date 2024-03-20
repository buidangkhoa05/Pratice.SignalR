using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;
using WebAPI;
public class RealTimeHub : Hub
{
    private ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();

    public async Task NewMessage(string user, string message)
    {
        await Clients.All.SendAsync("messageReceived", user, message);
    }

    public async Task SendUpdatedData(string dataType, string data)
    {
        await Clients.All.SendAsync("dataUpdated", dataType, data);
    }

    public override Task OnConnectedAsync()
    {
        var clientId = Context.ConnectionId;
        var serialId = (string) Context.GetHttpContext().Request.Query["searialId"];

        _connections.TryAdd(serialId, clientId);
        Console.WriteLine($"SingalR connected : {clientId}: {serialId}");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var clientId = Context.ConnectionId;
        var serialId = (string)Context.GetHttpContext().Request.Query["searialId"];

        Console.WriteLine($"SingalR disconnected : {clientId}: {serialId}");

        _connections.TryRemove(serialId, out _);

        return base.OnDisconnectedAsync(exception);
    }
}