using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using shaker.data;
using shaker.data.entity.Users;

namespace shaker.domain.Users.Identity
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly IUnitOfWork _uow;
        public RoleStore(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region IDisposable Support
        public virtual void Dispose()
        {
            _uow.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            role.Id = _uow.Roles.Add(role);

            IdentityResult result = IdentityResult.Failed();

            if (!string.IsNullOrEmpty(role.Id))
            {
                result = IdentityResult.Success;
                _uow.Commit();
            }

            return Task.FromResult(result);
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            bool state = _uow.Roles.Remove(role);

            IdentityResult result = IdentityResult.Failed();

            if (state)
            {
                result = IdentityResult.Success;
                _uow.Commit();
            }

            return Task.FromResult(result);
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_uow.Roles.Get(roleId));
        }

        public Task<Role> FindByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            string normalizedRoleName = roleName.ToUpperInvariant();
            return Task.FromResult(_uow.Roles.Get(r => r.NormalizedName == normalizedRoleName));
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

            bool state = _uow.Roles.Update(role);

            if (state) _uow.Commit();

            return Task.FromResult(0);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;

            bool state = _uow.Roles.Update(role);

            if (state) _uow.Commit();

            return Task.FromResult(0);
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            bool state = _uow.Roles.Update(role);

            IdentityResult result = IdentityResult.Failed();

            if (state)
            {
                result = IdentityResult.Success;
                _uow.Commit();
            }

            return Task.FromResult(result);
        }
    }
}
