using System.Collections.Generic;
using System.Threading.Tasks;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public interface IChannelsDomain
    {
        Task<ChannelDto> Create(ChannelDto channel);

        Task<bool> Delete(int id);

        Task<ChannelDto> Get(int id, bool withMessages = false);

        Task<IEnumerable<ChannelDto>> GetAll();
    }
}
