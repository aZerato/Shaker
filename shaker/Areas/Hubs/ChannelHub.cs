using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using shaker.crosscutting.Accessors;
using shaker.domain.Channels;
using shaker.domain.dto.Channels;

namespace shaker.Areas.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChannelHub : Hub
    {
        private readonly IConnectedUserAccessor _connectedUserAccessor;
        private readonly IMessagesDomain _messageDomain;
        public static string Path = "/hub/channel";

        public ChannelHub(
            IConnectedUserAccessor connectedUserAccessor,
            IMessagesDomain messageDomain)
        {
            _connectedUserAccessor = connectedUserAccessor;
            _messageDomain = messageDomain;
        }

        public async Task BroadcastMessage(string messageJson)
        {
            MessageDto dto = JsonConvert.DeserializeObject<MessageDto>(messageJson);

            await Task.Run(() => dto = _messageDomain.Create(dto));

            messageJson = JsonConvert.SerializeObject(dto);

            await Clients.All.SendAsync("BroadcastMessage", messageJson);
        }

        public async Task RegisterConnection()
        {   
            await Clients.All.SendAsync("RegisterConnection", _connectedUserAccessor.GetId());
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}
