using System.Collections.Generic;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public interface IMessagesDomain
    {
        MessageDto Create(MessageDto message);

        bool DeleteOne(string id);

        bool DeleteAll(string channelId);

        IEnumerable<MessageDto> GetAll(string channelId);
    }
}
