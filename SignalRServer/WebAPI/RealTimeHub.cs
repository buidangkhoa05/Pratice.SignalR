using Microsoft.AspNetCore.SignalR;
public class RealTimeHub : Hub
{

    public async Task NewMessage(string user, string message)
    {
        var clientID = Clients.Caller.
        await Clients.All.SendAsync("messageReceived", user, message);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine("SingalR connected");
        return base.OnConnectedAsync();
    }
}