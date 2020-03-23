using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using shaker.Areas.WebSocketArea.Handlers;
using shaker.Areas.WebSocketArea.Models;
using shaker.Areas.WebSocketArea.Modules.ChatModule.Models;
using shaker.Areas.WebSocketArea.Repositories;

namespace shaker.Areas.WebSocketArea.Modules.ChatModule.Handlers
{
    public class ChatWebSocketHandler : IWebSocketMessageHandler
    {
        private readonly IWebSocketRepository _wsRepository;

        public string Path { get; set; }

        public ChatWebSocketHandler(IWebSocketRepository webSocketRepository, string path = "/ws/chat") 
        {
            _wsRepository = webSocketRepository;
            Path = path;
        }

        public async Task BroadcastAll(string msg, byte[] buffer, WebSocketMessage webSocketMessage)
        {
            var all = _wsRepository.GetAll();

            foreach (var uws in all)
            {
                await uws.Ws.SendAsync(new ArraySegment<byte>(buffer, 0, msg.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
            }
        }

        public async Task BroadcastOthers(string msg, byte[] buffer, WebSocketMessage webSocketMessage)
        {
            var others = _wsRepository.GetAllOthers(webSocketMessage);

            foreach (var uws in others)
            {
                await uws.Ws.SendAsync(new ArraySegment<byte>(buffer, 0, msg.Length), 
                    WebSocketMessageType.Text, 
                    true, 
                    CancellationToken.None);
            }
        }

        public async Task HandleMessage(WebSocketReceiveResult result, byte[] buffer, WebSocketMessage webSocketMessage)
        {
            try
            {
                string msg = Encoding.ASCII.GetString(buffer);
                ChatWsMessage message = JsonConvert.DeserializeObject<ChatWsMessage>(msg);
                string serialisedMessage = JsonConvert.SerializeObject(message);
                byte[] bufferUpadted = Encoding.ASCII.GetBytes(serialisedMessage);

                if (message.Type == ChatWsMessageType.Message)
                {
                    await BroadcastOthers(serialisedMessage, bufferUpadted, webSocketMessage);
                }
            }
            catch (Exception)
            {
                await webSocketMessage.Ws.SendAsync(
                    new ArraySegment<byte>(buffer, 0, result.Count), 
                    result.MessageType, 
                    result.EndOfMessage, 
                    CancellationToken.None);
            }
        }

        public async Task SendInitialMessages(WebSocketMessage webSocketMessage)
        {
            WebSocket webSocket = webSocketMessage.Ws;
            ChatWsMessage msg = new ChatWsMessage
            {
                Type = ChatWsMessageType.System,
                Text = "Welcome",
                Username = "system"
            };

            string serialisedMessage = JsonConvert.SerializeObject(msg, Formatting.Indented);

            byte[] bytes = Encoding.ASCII.GetBytes(serialisedMessage);

            await webSocket.SendAsync(
                new ArraySegment<byte>(bytes, 0, serialisedMessage.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }
    }
}
