using System.Net.WebSockets;
using System.Threading.Tasks;
using shaker.Areas.WebSocketArea.Models;

namespace shaker.Areas.WebSocketArea.Handlers
{
    public interface IWebSocketMessageHandler
    {
        string Path { get; set; }

        Task SendInitialMessages(WebSocketMessage webSocketMessage);

        Task HandleMessage(WebSocketReceiveResult result, byte[] buffer, WebSocketMessage webSocketMessage);

        Task BroadcastOthers(string msg, byte[] buffer, WebSocketMessage webSocketMessage);

        Task BroadcastAll(string msg, byte[] buffer, WebSocketMessage webSocketMessage);
    }
}
