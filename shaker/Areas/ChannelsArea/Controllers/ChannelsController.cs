using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Channels;
using shaker.domain.dto.Channels;

namespace shaker.Areas.ChannelsArea.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        private readonly IChannelsDomain _channelsDomain;
        private readonly ILogger<ChannelsController> _logger;

        public ChannelsController(
            IChannelsDomain channelsDomain,
            ILogger<ChannelsController> logger
            )
        {
            _channelsDomain = channelsDomain;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ChannelDto> Get()
        {
            return _channelsDomain.GetAll();
        }

        [HttpGet("{id}")]
        public ChannelDto Get(int id, bool msgs)
        {
            return _channelsDomain.Get(id, msgs);
        }

        [HttpPost]
        public ChannelDto Post([FromBody]ChannelDto dto)
        {
            return _channelsDomain.Create(dto);
        }

        [HttpPut("{id}")]
        public ChannelDto Put(int id, [FromBody]ChannelDto dto)
        {
            return _channelsDomain.Update(id, dto);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _channelsDomain.Delete(id);
        }
    }
}
