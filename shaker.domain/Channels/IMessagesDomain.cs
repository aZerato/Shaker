using System.Collections.Generic;
using System.Threading.Tasks;
using shaker.domain.dto.Channels;

namespace shaker.domain.Channels
{
    public interface IMessagesDomain
    {
        Task<MessageDto> Create(MessageDto message);

        Task<bool> Delete(int channelId);

        Task<IEnumerable<MessageDto>> GetAll(int channelId);
    }
}
