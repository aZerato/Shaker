using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using shaker.data.core;
using shaker.data.entity.Channels;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public class MessagesDomain : IMessagesDomain
    {
        private IRepository<Message> _repository;

        public MessagesDomain(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public async Task<MessageDto> Create(MessageDto dto)
        {
            Message entity = new Message() {
                ChannelId = dto.ChannelId,
                UserId = dto.UserId,
                Content = dto.Content,
                Creation = DateTime.UtcNow
            };

            Task<int> addMessageTask = Task.Run(() => _repository.Add(entity));

            await addMessageTask;

            dto.Id = addMessageTask.IsCompletedSuccessfully ? addMessageTask.Result : 0;

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            Task<Message> getMessageTask = Task.Run(() => _repository.Get(id));

            Task<bool> removeMessageTask = getMessageTask.ContinueWith(getMsgTsk =>
            {
                if (getMsgTsk.IsCompletedSuccessfully)
                {
                    return _repository.Remove(getMessageTask.Result);
                }
                return false;
            });

            return await removeMessageTask;
        }

        public async Task<IEnumerable<MessageDto>> GetAll(int channelId)
        {
            Task<IEnumerable<MessageDto>> getAllMessagesTask =
                Task.Run(() =>
                {
                    return  _repository.GetAll(
                                ToMessageDtoSb(),
                                msg => msg.ChannelId == channelId); 
                });

            return await getAllMessagesTask;
        }

        private static Expression<Func<Message, MessageDto>> ToMessageDtoSb()
        {
            return msg => new MessageDto
            {
                Id = msg.Id,
                ChannelId = msg.ChannelId,
                UserId = msg.UserId,
                Content = msg.Content,
                Creation = msg.Creation
            };
        }
    }
}
