using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.domain.Users
{
    public class IndexChecks
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public IndexChecks(
            IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void EnsureUniqueIndexOnUserName()
        {
            _userRepository.EnsureUniqueIndex(nameof(User.UserName));
        }

        public void EnsureUniqueIndexOnRoleName()
        {
            _roleRepository.EnsureUniqueIndex(nameof(Role.Name));
        }

        public void EnsureUniqueIndexOnEmail()
        {
            _userRepository.EnsureUniqueIndex(nameof(User.Email));
        }
    }
}
