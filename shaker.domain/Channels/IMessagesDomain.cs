using System.Collections.Generic;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public interface IMessagesDomain
    {
        MessageDto Create(MessageDto message);

        bool DeleteOne(int id);

        bool DeleteAll(int channelId);

        IEnumerable<MessageDto> GetAll(int channelId);
    }
}
