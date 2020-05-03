using System.Collections.Generic;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public interface IChannelsDomain
    {
        ChannelDto Create(ChannelDto channel);

        bool Delete(string id);

        ChannelDto Get(string id, bool withMessages = false);

        ChannelDto Update(string id, ChannelDto dto);

        IEnumerable<ChannelDto> GetAll();
    }
}
