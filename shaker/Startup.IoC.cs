using Microsoft.Extensions.DependencyInjection;
using shaker.Areas.WebSocketArea.Modules.ChatModule.Handlers;
using shaker.Areas.WebSocketArea.Repositories;
using shaker.data.core;
using shaker.data.entity;
using shaker.data.entity.Channels;
using shaker.data.Json;
using shaker.domain.Channels;
using shaker.domain.Posts;

namespace shaker
{
    public partial class Startup
    {
        public void ConfigureServicesIoC(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork>(s => new JsonUnitOfWork(@"bd.json"));

            services.AddTransient<IRepository<Post>, Repository<Post>>();
            services.AddTransient<IPostsDomain, PostsDomain>();

            services.AddTransient<IRepository<Message>, Repository<Message>>();
            services.AddTransient<IMessagesDomain, MessagesDomain>();
            services.AddTransient<IRepository<Channel>, Repository<Channel>>();
            services.AddTransient<IChannelsDomain, ChannelsDomain>();

            services.AddSingleton<IWebSocketRepository, WebSocketRepository>();
            services.AddSingleton<ChatWebSocketHandler>();
        }
    }
}
