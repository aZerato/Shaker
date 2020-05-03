using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.domain.Users
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly IRepository<Role> _rolesRepository;

        public RoleStore(IRepository<Role> rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            role.Id = _rolesRepository.Add(role);

            IdentityResult result = IdentityResult.Failed();

            if (!string.IsNullOrEmpty(role.Id)) result = IdentityResult.Success;

            return Task.FromResult(result);
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            bool state = _rolesRepository.Remove(role);

            IdentityResult result = IdentityResult.Failed();

            if (state) result = IdentityResult.Success;

            return Task.FromResult(result);
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_rolesRepository.Get(roleId));
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(_rolesRepository.Get(r => r.NormalizedName == normalizedRoleName));
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;

            return Task.FromResult(_rolesRepository.Update(role));
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(_rolesRepository.Update(role));
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            bool state = _rolesRepository.Update(role);

            IdentityResult result = IdentityResult.Failed();

            if (state) result = IdentityResult.Success;

            return Task.FromResult(result);
        }
    }
}
