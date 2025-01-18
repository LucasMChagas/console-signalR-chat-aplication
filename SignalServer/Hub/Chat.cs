namespace SignalServer.Hub;
using Microsoft.AspNetCore.SignalR;

public class Chat : Hub
{
    public void NewMessage(string userName, string message)
    {
        Clients.All.SendAsync("NewMessage", userName, message);
    }
}