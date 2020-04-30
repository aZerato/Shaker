using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using shaker.data.core;
using shaker.data.entity.Channels;
using shaker.domain.Channels;
using shaker.domain.dto.Channels;

namespace shaker.domain.Posts
{
    public class ChannelsDomain : IChannelsDomain
    {
        private IRepository<Channel> _channelRepository;
        private IMessagesDomain _messagesDomain;

        public ChannelsDomain(
            IRepository<Channel> channelRepository,
            IMessagesDomain messagesDomain)
        {
            _channelRepository = channelRepository;
            _messagesDomain = messagesDomain;
        }

        public ChannelDto Create(ChannelDto dto)
        {
            Channel entity = new Channel() {
                Name = dto.Description,
                Description = dto.Description,
                ImgPath = dto.ImgPath,
                Creation = DateTime.UtcNow
            };

            dto.Id = _channelRepository.Add(entity);

            return dto;
        }

        public bool Delete(int id)
        {
            Channel ch = _channelRepository.Get(id);

            if (ch == null) return false;

            return _channelRepository.Remove(ch) && _messagesDomain.DeleteAll(id);
        }

        public ChannelDto Get(int id, bool withMessages)
        {
            Channel entity = _channelRepository.Get(id);
            return new ChannelDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImgPath = entity.ImgPath,
                Creation = entity.Creation,
                Messages = withMessages ? _messagesDomain.GetAll(id) : null
            };
        }

        public IEnumerable<ChannelDto> GetAll()
        {
            return _channelRepository.GetAll(ToChannelDtoSb());
        }

        private static Expression<Func<Channel, ChannelDto>> ToChannelDtoSb()
        {
            return s => new ChannelDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                ImgPath = s.ImgPath,
                Creation = s.Creation
            };
        }
    }
}
