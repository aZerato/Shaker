using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using shaker.crosscutting.Accessors;
using shaker.data;
using shaker.data.entity.Channels;
using shaker.data.entity.Planning;
using shaker.data.entity.Users;
using shaker.domain.dto.Channels;

namespace shaker.domain.Planning
{
    public class PlanningDomain : IPlanningDomain
    {
        private IUnitOfWork _uow;
        private IConnectedUserAccessor _connectedUserAccessor;

        public PlanningDomain(
            IUnitOfWork uow,
            IConnectedUserAccessor connectedUserAccessor)
        {
            _uow = uow;
            _connectedUserAccessor = connectedUserAccessor;
        }

        public CalendarEventDto Create(CalendarEventDto dto)
        {
            CalendarEvent entity = new CalendarEvent() {

                User = new User() { Id = _connectedUserAccessor.GetId() },
                Content = dto.Content,
                Creation = DateTime.UtcNow.Date
            };

            _uow.Messages.Add(entity);
            _uow.Commit();

            dto = ToMessageDto(entity);

            return dto;
        }

        public bool DeleteOne(string id)
        {
            Message msg = _uow.Messages.Get(id);

            if (msg == null) return false;

            return _uow.Messages.Remove(msg);
        }

        public bool DeleteAll(string channelId)
        {
            IEnumerable<Message> msgs = _uow.Messages.GetAll(m => m.Channel.Id == channelId);

            bool state = msgs == null || !msgs.Any() ? true : false;
            foreach(Message msg in msgs)
            {
                state = _uow.Messages.Remove(msg);
            }

            return state;
        }

        public IEnumerable<MessageDto> GetAll(string channelId)
        {
            return _uow.Messages.GetAll(ToMessageDtoSb(), m => m.Channel.Id == channelId);
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

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _uow.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
