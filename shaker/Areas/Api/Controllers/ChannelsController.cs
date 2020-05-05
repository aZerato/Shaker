using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.crosscutting.Messages;
using shaker.domain.Channels;
using shaker.domain.dto.Channels;

namespace shaker.Areas.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ChannelsController : Controller, IDisposable
    {
        private readonly IChannelsDomain _channelsDomain;
        private readonly IBotDomain _botDomain;
        private readonly ILogger<ChannelsController> _logger;

        public ChannelsController(
            IChannelsDomain channelsDomain,
            IBotDomain botDomain,
            ILogger<ChannelsController> logger
            )
        {
            _channelsDomain = channelsDomain;
            _botDomain = botDomain;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ChannelDto> Get()
        {
            return _channelsDomain.GetAll();
        }

        [HttpGet("{id}")]
        public ChannelDto Get(string id, bool msgs)
        {
            try
            {
                return _channelsDomain.Get(id, msgs);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new ChannelDto() { Error = MessagesGetter.Get(ErrorPresentationMessages.DefaultErrorMessage) };
            }
        }

        [HttpPost]
        public ChannelDto Post([FromBody]ChannelDto dto)
        {
            try
            {
                dto = _channelsDomain.Create(dto);

                _botDomain.CreateWelcomeMessage(dto.Id);

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new ChannelDto() { Error = MessagesGetter.Get(ErrorPresentationMessages.DefaultErrorMessage) };
            }
        }

        [HttpPut("{id}")]
        public ChannelDto Put(string id, [FromBody]ChannelDto dto)
        {
            try
            {
                return _channelsDomain.Update(id, dto);

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new ChannelDto() { Error = MessagesGetter.Get(ErrorPresentationMessages.DefaultErrorMessage) };
            }
        }

        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _channelsDomain.Delete(id);
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            _channelsDomain.Dispose();
            _botDomain.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
