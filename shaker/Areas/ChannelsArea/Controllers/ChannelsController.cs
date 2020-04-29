using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMessagesDomain _messagesDomain;
        private readonly ILogger<ChannelsController> _logger;

        public ChannelsController(
            IChannelsDomain channelsDomain,
            IMessagesDomain messagesDomain,
            ILogger<ChannelsController> logger
            )
        {
            _channelsDomain = channelsDomain;
            _messagesDomain = messagesDomain;
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<ChannelDto>> Get()
        {
            //var dto = new MessageDto();
            //dto.ChannelId = 1;
            //dto.Content = "test";
            //dto.UserId = 1;
            //await _messagesDomain.Create(dto);

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
        public void Post([FromBody]string value)
        {
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
