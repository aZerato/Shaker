using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Channels;
using shaker.domain.dto.Channels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shaker.Areas.ChannelsArea.Controllers
{
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

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<ChannelDto>> Get()
        {
            return await _channelsDomain.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ChannelDto> Get(int id, bool msgs)
        {
            return await _channelsDomain.Get(id, msgs);
        }

        // POST api/values
        [HttpPost]
        public Task<ChannelDto> Post([FromBody]ChannelDto dto)
        {
            return await _channelsDomain.Create(dto);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
