using System.Collections.Generic;

namespace shaker.domain.Users
{
    public interface IUsersDomain
    {
        UserDto IsAuthenticated(AuthDto dto);

        UserDto Create(AuthDto dto);

        bool Delete(string id);

        UserDto Get(string id);

        IEnumerable<UserDto> GetAll();

        bool Update(UserDto dto);
    }
}
