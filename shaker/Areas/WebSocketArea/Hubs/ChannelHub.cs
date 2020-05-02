using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using shaker.domain.dto.Channels;

namespace shaker.Areas.WebSocketArea.Hubs
{
    public class ChannelHub : Hub
    {
        public ChannelHub()
        {

        }

        public async Task BroadcastMessage(string content)
        {
            var dto = new MessageDto();
            dto.Content = content;
            await Clients.All.SendAsync("BroadcastMessage", dto);
        }
    }
}
