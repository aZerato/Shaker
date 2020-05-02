using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using shaker.data.core;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public class MessagesDomain : IMessagesDomain
    {
        private IRepository<Message> _repository;

        public MessagesDomain(
            IRepository<Message> repository)
        {
            _repository = repository;
        }

        public MessageDto Create(MessageDto dto)
        {
            Channel ch = new Channel();
            ch.Id = dto.ChannelId;
            Message entity = new Message() {
                Channel = ch,
                User = new User() { Id = dto.UserId },
                Content = dto.Content,
                Creation = DateTime.UtcNow.Date
            };

            dto.Id = _repository.Add(entity);

            return dto;
        }

        public bool DeleteOne(int id)
        {
            Message msg = _repository.Get(id);

            if (msg == null) return false;

            return _repository.Remove(msg);
        }

        public bool DeleteAll(int channelId)
        {
            IEnumerable<Message> msgs = _repository.GetAll(m => m.Channel.Id == channelId);

            bool state = msgs == null || !msgs.Any() ? true : false;
            foreach(Message msg in msgs)
            {
                state = _repository.Remove(msg);
            }

            return state;
        }

        public IEnumerable<MessageDto> GetAll(int channelId)
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
    }
}
