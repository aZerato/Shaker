using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public async Task<ChannelDto> Create(ChannelDto dto)
        {
            Channel entity = new Channel() {
                Name = dto.Description,
                Description = dto.Description,
                ImgPath = dto.ImgPath,
                Creation = DateTime.UtcNow
            };

            Task<int> addChannelTask = Task.Run(() => {
                return _channelRepository.Add(entity);
            });

            await Task.WhenAll(addChannelTask);

            dto.Id = addChannelTask.IsCompleted ? addChannelTask.Result : 0;

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            Task<Channel> getChannelTask = Task.Run(() => _channelRepository.Get(id));

            Task<bool> removeChannelTask =
                getChannelTask.ContinueWith(getChTsk => {
                        if (getChannelTask.IsCompletedSuccessfully && getChannelTask.Result != null)
                        {
                            return _channelRepository.Remove(getChannelTask.Result);
                        }
                        return false;  
                    });

            Task<bool> removeMsgs = _messagesDomain.Delete(id);

            Task<bool> removeChAndMsgs = removeChannelTask.ContinueWith((rmvChTsk) => {
                    if (removeChannelTask.IsCompletedSuccessfully && removeChannelTask.Result)
                    {
                        return true;
                    }
                    return false;
                });

            await Task.WhenAll(removeChAndMsgs, removeMsgs);

            return removeMsgs.Result;
        }

        public async Task<ChannelDto> Get(int id, bool withMessages)
        {
            Task<ChannelDto> getChannelTask = Task.Run(() =>
            {
                Channel entity = _channelRepository.Get(id);
                return new ChannelDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    ImgPath = entity.ImgPath,
                    Creation = entity.Creation
                };
            });

            if (withMessages)
            {
                Task<ChannelDto> getDtoWithMessagesTask = await getChannelTask.ContinueWith(async (getChtsk) =>
                 {
                     if (getChannelTask.IsCompletedSuccessfully && getChannelTask.Result != null)
                     {
                         Task<IEnumerable<MessageDto>> getMsgsTsk = _messagesDomain.GetAll(id);

                         await getMsgsTsk;

                         getChtsk.Result.Messages = getMsgsTsk.Result;

                         return getChtsk.Result;
                     }
                     return null;
                 });

                return await getDtoWithMessagesTask;
            }
            else
            {
                return await getChannelTask;
            }
        }

        public async Task<IEnumerable<ChannelDto>> GetAll()
        {
            Task<IEnumerable<ChannelDto>> getAllTsk =
                Task.Run(() =>
                {
                    return _channelRepository.GetAll(
                                ToChannelDtoSb());
                });

            return await getAllTsk;
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
