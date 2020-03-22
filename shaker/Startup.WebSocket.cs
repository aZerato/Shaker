using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using shaker.Areas.WebSocketArea.Handlers;
using shaker.Areas.WebSocketArea.Middleware;
using shaker.Areas.WebSocketArea.Modules.ChatModule.Handlers;
using shaker.Areas.WebSocketArea.Repositories;

namespace shaker
{
    public partial class Startup
    {
        public void ConfigureWebSocket(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };

            app.UseWebSockets(webSocketOptions);

            IWebSocketMessageHandler chatHandler = (ChatWebSocketHandler) services.GetService(typeof(ChatWebSocketHandler));
            IWebSocketRepository repository = (IWebSocketRepository) services.GetService(typeof(IWebSocketRepository));
            app.Map(chatHandler.Path, (_app) =>
                _app.UseMiddleware<WebSocketMessageMiddleware>(chatHandler, repository));
        }
    }
}
