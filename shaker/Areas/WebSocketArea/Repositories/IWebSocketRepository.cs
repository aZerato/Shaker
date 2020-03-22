using System.Collections.Generic;
using System.Net.WebSockets;
using shaker.Areas.WebSocketArea.Models;

namespace shaker.Areas.WebSocketArea.Repositories
{
    public interface IWebSocketRepository
    {
        WebSocketMessage Add(WebSocket webSocket);

        WebSocketMessage Remove(string webSocketId);

        IEnumerable<WebSocketMessage> GetAll();

        IEnumerable<WebSocketMessage> GetAllOthers(WebSocketMessage webSocketMsg);
    }
}
