using System.Collections.Generic;

namespace shaker.domain.Users
{
    public interface IUsersDomain
    {
        bool IsAuthenticated(AuthDto dto);

        UserDto Create(AuthDto dto);

        bool Delete(int id);

        UserDto Get(int id);

        IEnumerable<UserDto> GetAll();
    }
}
