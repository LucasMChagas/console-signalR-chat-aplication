using Microsoft.AspNetCore.SignalR;

namespace SignalServer;

// public class ChatHub : Hub
// {
//     public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
//     {
//         while (true)
//         {
//             yield return DateTime.UtcNow;
//             await Task.Delay(1000, cancellationToken);
//         }
//     }
// }