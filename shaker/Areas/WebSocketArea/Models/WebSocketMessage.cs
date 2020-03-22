using System;
using System.Net.WebSockets;

namespace shaker.Areas.WebSocketArea.Models
{
    public class WebSocketMessage
    {
        public WebSocketMessage(WebSocket ws)
        {
            Ws = ws;
            Id = Guid.NewGuid().ToString();
        }

        public WebSocket Ws { get; set; }

        public string Id { get; set; }
    }
}
