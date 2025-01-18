using SignalServer;
using SignalServer.Hub;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();


var app = builder.Build();

app.MapGet("/", () => "Status: ok");

app.MapHub<Chat>("/chat");


app.Run();
