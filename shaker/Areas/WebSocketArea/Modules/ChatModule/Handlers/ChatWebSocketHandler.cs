using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using shaker.Areas.WebSocketArea.Handlers;
using shaker.Areas.WebSocketArea.Models;
using shaker.Areas.WebSocketArea.Repositories;
using shaker.domain.dto.Channels;

namespace shaker.Areas.WebSocketArea.Modules.ChatModule.Handlers
{
    public class ChatWebSocketHandler : IWebSocketMessageHandler
    {
        private readonly IWebSocketRepository _wsRepository;

        public string Path { get; set; }

        public ChatWebSocketHandler(
            IWebSocketRepository webSocketRepository,
            string path = "/ws/channel") 
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
                MessageDto message = JsonConvert.DeserializeObject<MessageDto>(msg);
                string serialisedMessage = JsonConvert.SerializeObject(message);
                byte[] bufferUpadted = Encoding.ASCII.GetBytes(serialisedMessage);

                if (message.Type == MessageType.Message)
                {
                    //await BroadcastOthers(serialisedMessage, bufferUpadted, webSocketMessage);
                    await BroadcastAll(serialisedMessage, bufferUpadted, webSocketMessage);
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

            await WelcomeMessage(webSocket);

            await UserConnectedMessageToOthers(webSocketMessage);
        }

        private async Task WelcomeMessage(WebSocket webSocket)
        {
            MessageDto msg = new MessageDto();
            msg.Content = "Welcome";
            msg.Type = MessageType.System;

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string serialisedMessage = JsonConvert.SerializeObject(msg, Formatting.Indented, serializerSettings);

            byte[] bytes = Encoding.ASCII.GetBytes(serialisedMessage);

            await webSocket.SendAsync(
                new ArraySegment<byte>(bytes, 0, serialisedMessage.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }

        private async Task UserConnectedMessageToOthers(WebSocketMessage webSocketMessage)
        {
            MessageDto msg = new MessageDto();
            msg.Content = "Welcome to a new user";
            msg.Type = MessageType.System;

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string serialisedMessage = JsonConvert.SerializeObject(msg, Formatting.Indented, serializerSettings);

            byte[] bytes = Encoding.ASCII.GetBytes(serialisedMessage);

            var others = _wsRepository.GetAllOthers(webSocketMessage);

            foreach (var uws in others)
            {
                await uws.Ws.SendAsync(new ArraySegment<byte>(bytes, 0, serialisedMessage.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
            }
        }
    }
}
