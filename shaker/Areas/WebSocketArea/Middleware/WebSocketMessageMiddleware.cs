using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace shaker.Areas.WebSocketArea.Middleware
{
    using Handlers;
    using Models;
    using Repositories;

    public class WebSocketMessageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebSocketMessageHandler _wsHandler;
        private readonly IWebSocketRepository _wsRepository;

        public WebSocketMessageMiddleware(RequestDelegate next,
                                    IWebSocketMessageHandler webSocketMessageHandler,
                                    IWebSocketRepository webSocketRepository)
        {
            _next = next;
            _wsHandler = webSocketMessageHandler;
            _wsRepository = webSocketRepository;
        }

        public async Task Invoke(HttpContext context)
        {
            // context.Request.Path == _wsHandler.Path
            if (context.Request.PathBase == _wsHandler.Path)
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                    WebSocketMessage webSocketMessage = _wsRepository.Add(webSocket);

                    await _wsHandler.SendInitialMessages(webSocketMessage);

                    await Listen(context, webSocketMessage);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            }

            await _next(context);
        }

        private async Task Listen(HttpContext context, WebSocketMessage webSocketMessage)
        {
            var buffer = new byte[1024 * 4];
            
            WebSocketReceiveResult result = await webSocketMessage.Ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                await _wsHandler.HandleMessage(result, buffer, webSocketMessage);

                buffer = new byte[1024 * 4];

                result = await webSocketMessage.Ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            _wsRepository.Remove(webSocketMessage.Id);

            await webSocketMessage.Ws.CloseAsync(result.CloseStatus.Value, 
                                                    result.CloseStatusDescription, 
                                                    CancellationToken.None);
        }
    }
}
