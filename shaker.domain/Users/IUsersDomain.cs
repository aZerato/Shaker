using System;
using System.Collections.Generic;
using shaker.domain.dto.Users;

namespace shaker.domain.Users
{
    public interface IUsersDomain : IDisposable
    {
        UserDto Authenticate(AuthDto dto);

        void Logout();

        UserDto Create(SignInDto dto);

        bool Delete(string id);

        UserDto Get(string id);

        IEnumerable<UserDto> GetAll();

        bool Update(UserDto dto);
    }
}
