using System.Collections.Generic;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public interface IChannelsDomain
    {
        ChannelDto Create(ChannelDto channel);

        bool Delete(int id);

        ChannelDto Get(int id, bool withMessages = false);

        IEnumerable<ChannelDto> GetAll();
    }
}
