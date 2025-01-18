using Microsoft.AspNetCore.SignalR.Client;

var uri = "http://localhost:5229/chat";

await using var connection = new HubConnectionBuilder().WithUrl(uri).Build();

connection.On<string, string>("NewMessage", (userName, message) =>
{
    Console.WriteLine($"{userName}: {message}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Conectado ao servidor SignalR.");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao conectar: {ex.Message}");
    return;
}

// Enviar mensagens ao servidor
Console.WriteLine("Digite seu nome!");
var name = Console.ReadLine();
Console.WriteLine("Digite a mensagem ou 'sair' para encerrar:");
string input;
while ((input = Console.ReadLine()) != null)
{
    if (input.Equals("sair", StringComparison.OrdinalIgnoreCase))
        break;
   

    string userName = name;
    string message = input;

    try
    {
        await connection.InvokeAsync("NewMessage", userName, message);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
    }
}

// Encerrar a conexão
await connection.StopAsync();
Console.WriteLine("Conexão encerrada.");




