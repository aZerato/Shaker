using System.Security.Principal;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using shaker.Areas.Api.Auth;
using shaker.crosscutting.Accessors;
using shaker.data;
using shaker.data.core;
using shaker.data.entity.Posts;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.data.Json;
using shaker.domain.Channels;
using shaker.domain.Posts;
using shaker.domain.Users;
using shaker.domain.Users.Identity;
using shaker.data.entity.Planning;
using shaker.domain.Planning;
using shaker.data.entity.Movements;
using shaker.domain.Movements;

namespace shaker
{
    public partial class Startup
    {
        public void ConfigureServicesIoC(IServiceCollection services)
        {
            services.AddScoped<ILiteDatabase>(s => new LiteDatabase(@"bd.json"));
            services.AddScoped<IDbContext, JsonDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<IPostsDomain, PostsDomain>();
            services.AddScoped<IDbSet<Post>, JsonDbSet<Post>>();

            services.AddScoped<IRepository<Message>, Repository<Message>>();
            services.AddScoped<IMessagesDomain, MessagesDomain>();
            services.AddScoped<IRepository<Channel>, Repository<Channel>>();
            services.AddScoped<IChannelsDomain, ChannelsDomain>();
            services.AddScoped<IBotDomain, BotDomain>();
            services.AddScoped<IDbSet<Message>, JsonDbSet<Message>>();
            services.AddScoped<IDbSet<Channel>, JsonDbSet<Channel>>();

            services.AddScoped<IRepository<CalendarEvent>, Repository<CalendarEvent>>();
            services.AddScoped<IRepository<CalendarEventType>, Repository<CalendarEventType>>();
            services.AddScoped<IPlanningDomain, PlanningDomain>();
            services.AddScoped<IDbSet<CalendarEvent>, JsonDbSet<CalendarEvent>>();
            services.AddScoped<IDbSet<CalendarEventType>, JsonDbSet<CalendarEventType>>();

            services.AddScoped<IRepository<BodyPart>, Repository<BodyPart>>();
            services.AddScoped<IRepository<BodyZone>, Repository<BodyZone>>();
            services.AddScoped<IRepository<Movement>, Repository<Movement>>();
            services.AddScoped<IRepository<MovementBodyPart>, Repository<MovementBodyPart>>();
            services.AddScoped<IRepository<MovementBodyZone>, Repository<MovementBodyZone>>();
            services.AddScoped<IRepository<MovementType>, Repository<MovementType>>();
            services.AddScoped<IMovementsDomain, MovementsDomain>();
            services.AddScoped<IDbSet<BodyPart>, JsonDbSet<BodyPart>>();
            services.AddScoped<IDbSet<BodyZone>, JsonDbSet<BodyZone>>();
            services.AddScoped<IDbSet<Movement>, JsonDbSet<Movement>>();
            services.AddScoped<IDbSet<MovementBodyPart>, JsonDbSet<MovementBodyPart>>();
            services.AddScoped<IDbSet<MovementBodyZone>, JsonDbSet<MovementBodyZone>>();
            services.AddScoped<IDbSet<MovementType>, JsonDbSet<MovementType>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddScoped<IConnectedUserAccessor, ConnectedUserAccessor>();

            services.AddScoped<IDbSet<User>, JsonDbSet<User>>();
            services.AddScoped<IDbSet<Role>, JsonDbSet<Role>>();

            services.AddScoped<IJwtAuth, JwtAuth>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<IUserStore<User>, UserStore>();
            services.AddScoped<IRoleStore<Role>, RoleStore>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IUsersDomain, UsersDomain>();
        }
    }
}
