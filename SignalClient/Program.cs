using Microsoft.AspNetCore.SignalR.Client;

var uri = "http://localhost:5229/chat";

await using var connection = new HubConnectionBuilder().WithUrl(uri).Build();

connection.On<string, string>("ReceiveMessage", (userName, message) =>
{
    Console.WriteLine($"{userName}: {message}");
});


Console.WriteLine("Digite seu nome!");
var name = Console.ReadLine();
Console.WriteLine("Digite o nome do grupo que voce quer entrar!");
var grupo = Console.ReadLine();

try
{    
    await connection.StartAsync();
    await connection.InvokeAsync("JoinGroup", grupo);        
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao conectar: {ex.Message}");
    return;
}

// Enviar mensagens ao servidor


string input;
do
{
    Console.Write("--> : ");
    input = Console.ReadLine();

    if (input.Equals("sair", StringComparison.OrdinalIgnoreCase))
        break;

    string message = input;

    try
    {
        await connection.InvokeAsync("SendMessageToGroup", grupo, name, message);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
    }

} while (input != null);

// Encerrar a conexão
await connection.StopAsync();
Console.WriteLine("Conexão encerrada.");




