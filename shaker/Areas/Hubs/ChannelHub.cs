using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using shaker.domain.dto.Channels;
using Newtonsoft.Json;

namespace shaker.Areas.Hubs
{
    public class ChannelHub : Hub
    {
        public ChannelHub()
        {

        }

        public async Task BroadcastMessage(string messageJson)
        {
            MessageDto dto = JsonConvert.DeserializeObject<MessageDto>(messageJson);
            
            await Clients.All.SendAsync("BroadcastMessage", dto);
        }

        public async Task RegisterConnection(int userId)
        {
            
            await Clients.All.SendAsync("RegisterConnection", userId);
        }
    }
}
