using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using shaker.Areas.WebSocketArea.Models;

namespace shaker.Areas.WebSocketArea.Repositories
{
    public class WebSocketRepository : IWebSocketRepository
    {
        private ConcurrentDictionary<string, WebSocketMessage> _webSocketsDictionnary = new ConcurrentDictionary<string, WebSocketMessage>();

        public WebSocketMessage Add(WebSocket webSocket)
        {
            WebSocketMessage webSocketMessage = new WebSocketMessage(webSocket);

            _webSocketsDictionnary.TryAdd(webSocketMessage.Id, webSocketMessage);

            return webSocketMessage;
        }

        public WebSocketMessage Remove(string webSocketId)
        {
            _webSocketsDictionnary.TryRemove(webSocketId, out WebSocketMessage currentWsMsg);

            return currentWsMsg;
        }

        public IEnumerable<WebSocketMessage> GetAll()
        {
            return _webSocketsDictionnary
                        .Select(wsMsgDict => wsMsgDict.Value);
        }

        public IEnumerable<WebSocketMessage> GetAllOthers(WebSocketMessage webSocketMsg)
        {
            return _webSocketsDictionnary
                        .Where(wsMsgDict => wsMsgDict.Key != webSocketMsg.Id)
                        .Select(wsMsgDict => wsMsgDict.Value);
        }
    }
}
