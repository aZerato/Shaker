using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using shaker.crosscutting.Accessors;
using shaker.data.core;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public class MessagesDomain : IMessagesDomain
    {
        private IConnectedUserAccessor _connectedUserAccessor;
        private IRepository<Message> _repository;

        public MessagesDomain(
            IConnectedUserAccessor connectedUserAccessor,
            IRepository<Message> repository)
        {
            _connectedUserAccessor = connectedUserAccessor;
            _repository = repository;
        }

        public MessageDto Create(MessageDto dto)
        {
            Channel ch = new Channel();
            ch.Id = dto.ChannelId;
            Message entity = new Message() {
                Channel = ch,
                User = new User() { Id = _connectedUserAccessor.GetId() },
                Content = dto.Content,
                Creation = DateTime.UtcNow.Date
            };

            entity.Id = _repository.Add(entity);

            dto = ToMessageDto(entity);

            return dto;
        }

        public bool DeleteOne(string id)
        {
            Message msg = _repository.Get(id);

            if (msg == null) return false;

            return _repository.Remove(msg);
        }

        public bool DeleteAll(string channelId)
        {
            IEnumerable<Message> msgs = _repository.GetAll(m => m.Channel.Id == channelId);

            bool state = msgs == null || !msgs.Any() ? true : false;
            foreach(Message msg in msgs)
            {
                state = _repository.Remove(msg);
            }

            return state;
        }

        public IEnumerable<MessageDto> GetAll(string channelId)
        {
            return  _repository.GetAll(ToMessageDtoSb(), m => m.Channel.Id == channelId);
        }

        private static Expression<Func<Message, MessageDto>> ToMessageDtoSb()
        {
            return msg => new MessageDto
            {
                Id = msg.Id,
                ChannelId = msg.Channel.Id,
                UserId = msg.User.Id,
                Content = msg.Content,
                Creation = msg.Creation
            };
        }

        private static MessageDto ToMessageDto(Message msg)
        {
            return new MessageDto
            {
                Id = msg.Id,
                ChannelId = msg.Channel.Id,
                UserId = msg.User.Id,
                Content = msg.Content,
                Creation = msg.Creation
            };
        }
    }
}
