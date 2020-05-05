using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using shaker.data;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;

namespace shaker.domain.Channels
{
    public class BotDomain : IBotDomain
    {
        private readonly UserManager<User> _userManager;
        private IUnitOfWork _uow;

        public BotDomain(
            UserManager<User> userManager,
            IUnitOfWork uow)
        {
            _userManager = userManager;
            _uow = uow;
        }

        private User _botUser;
        public User GetBotUser()
        {
            if (_botUser != null)
                return _botUser;

            User bot = _userManager.FindByNameAsync("Bot").Result;
            if (bot != null)
                return _botUser = bot;

            _botUser = new User();
            _botUser.UserName = "Bot";
            _botUser.Email = "bot@shaker.com";

            _userManager.CreateAsync(_botUser);

            return _botUser;
        }

        public void CreateWelcomeMessage(string channelId)
        {
            Message message = new Message
            {
                Channel = new Channel() { Id = channelId },
                Creation = DateTime.UtcNow.Date,
                User = new User() { Id = GetBotUser().Id },
                Content = "Welcome to your new channel !"
            };

            _uow.Messages.Add(message);
            _uow.Commit();
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
