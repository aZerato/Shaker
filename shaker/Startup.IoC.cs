using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using shaker.Areas.Api.Auth;
using shaker.crosscutting.Accessors;
using shaker.data.core;
using shaker.data.entity;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.data.Json;
using shaker.domain.Channels;
using shaker.domain.Posts;
using shaker.domain.Users;

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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddTransient<IConnectedUserAccessor, ConnectedUserAccessor>();
            services.AddTransient<IJwtAuth, JwtAuth>();
            services.AddTransient<UserManager<User>>();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRepository<Role>, Repository<Role>>();
            services.AddTransient<IUsersDomain, UsersDomain>();
        }
    }
}
