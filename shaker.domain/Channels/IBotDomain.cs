using System;
using shaker.data.entity.Users;

namespace shaker.domain.Channels
{
    public interface IBotDomain : IDisposable
    {
        User GetBotUser();

        void CreateWelcomeMessage(string channelId);
    }
}
