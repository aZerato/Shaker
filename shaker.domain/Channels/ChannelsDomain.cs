using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using shaker.data;
using shaker.data.entity.Channels;
using shaker.domain.Channels;
using shaker.domain.dto.Channels;

namespace shaker.domain.Posts
{
    public class ChannelsDomain : IChannelsDomain
    {
        private readonly IUnitOfWork _uow;
        private readonly IMessagesDomain _messagesDomain;

        public ChannelsDomain(
            IUnitOfWork uow,
            IMessagesDomain messagesDomain)
        {
            _uow = uow;
            _messagesDomain = messagesDomain;
        }

        public ChannelDto Create(ChannelDto dto)
        {
            Channel entity = new Channel() {
                Name = dto.Name,
                Description = dto.Description,
                ImgPath = dto.ImgPath,
                Creation = DateTime.UtcNow.Date
            };

            string id = _uow.Channels.Add(entity);
            if (!string.IsNullOrEmpty(id))
                _uow.Commit();
            else
                _uow.RollbackChanges();

            return ToChannelDto(entity, false);
        }

        public bool Delete(string id)
        {
            Channel ch = _uow.Channels.Get(id);

            if (ch == null) return false;

            bool state = _uow.Channels.Remove(ch) && _messagesDomain.DeleteAll(id);
            if (state)
                _uow.Commit();
            else
                _uow.RollbackChanges();

            return state;
        }

        public ChannelDto Get(string id, bool withMessages)
        {
            Channel entity = _uow.Channels.Get(id);
            return ToChannelDto(entity, true);
        }

        public ChannelDto Update(string id, ChannelDto dto)
        {
            Channel entity = _uow.Channels.Get(id);

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.ImgPath = dto.ImgPath;

            bool state = _uow.Channels.Update(entity);
            if (state)
                _uow.Commit();
            else
                _uow.RollbackChanges();

            return ToChannelDto(entity, true);
        }

        public IEnumerable<ChannelDto> GetAll()
        {
            return _uow.Channels.GetAll(ToChannelDtoSb());
        }

        private ChannelDto ToChannelDto(Channel entity, bool withMessages)
        {
            return new ChannelDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImgPath = entity.ImgPath,
                Creation = entity.Creation,
                Messages = withMessages ? _messagesDomain.GetAll(entity.Id) : null
            };
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
