namespace SignalServer.Hub;
using Microsoft.AspNetCore.SignalR;

public class Chat : Hub
{
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", "Sistema", $"{Context.ConnectionId} entrou no grupo {groupName}");
    }

    // Remove um usuário de um grupo
    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", "Sistema", $"{Context.ConnectionId} saiu do grupo {groupName}");
    }

    // Envia uma mensagem para um grupo
    public async Task SendMessageToGroup(string groupName, string userName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", userName, message);
    }
    
}