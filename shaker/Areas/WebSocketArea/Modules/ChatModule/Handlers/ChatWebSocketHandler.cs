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

        public async Task BroadcastAll(byte[] buffer, WebSocketMessage webSocketMessage)
        {
            var all = _wsRepository.GetAll();

            foreach (var uws in all)
            {
                await uws.Ws.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
            }
        }

        public async Task BroadcastOthers(byte[] buffer, WebSocketMessage webSocketMessage)
        {
            var others = _wsRepository.GetAllOthers(webSocketMessage);

            foreach (var uws in others)
            {
                await uws.Ws.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), 
                    WebSocketMessageType.Text, 
                    true, 
                    CancellationToken.None);
            }
        }

        public async Task HandleMessage(WebSocketReceiveResult result, byte[] buffer, WebSocketMessage webSocketMessage)
        {
            string msg = Encoding.UTF8.GetString(buffer);

            try
            {
                ChatWsMessage message = JsonConvert.DeserializeObject<ChatWsMessage>(msg);

                if (message.Type == ChatWsMessageType.Message)
                {
                    await BroadcastOthers(buffer, webSocketMessage);
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

            string serialisedMessage = JsonConvert.SerializeObject(msg);
            serialisedMessage = serialisedMessage.Replace(@"""", @"\""");

            byte[] bytes = Encoding.UTF8.GetBytes(serialisedMessage);

            await webSocket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
