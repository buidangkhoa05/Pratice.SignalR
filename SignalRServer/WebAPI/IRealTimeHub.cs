using Microsoft.AspNetCore.SignalR;

namespace WebAPI
{
    public interface IRealTimeHub
    {
        Task SendUpdatedData(string dataType, string data);
    }
}
