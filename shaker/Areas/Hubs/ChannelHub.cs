using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;

namespace shaker.Areas.Hubs
{
    using shaker.domain.Channels;
    using shaker.domain.dto.Channels;

    public class ChannelHub : Hub
    {
        private readonly IMessagesDomain _messageDomain;

        public ChannelHub(
            IMessagesDomain messageDomain)
        {
            _messageDomain = messageDomain;
        }

        public async Task BroadcastMessage(string messageJson)
        {
            MessageDto dto = JsonConvert.DeserializeObject<MessageDto>(messageJson);

            await Task.Run(() => dto = _messageDomain.Create(dto));

            messageJson = JsonConvert.SerializeObject(dto);

            await Clients.All.SendAsync("BroadcastMessage", messageJson);
        }

        public async Task RegisterConnection(int userId)
        {
            
            await Clients.All.SendAsync("RegisterConnection", userId);
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
