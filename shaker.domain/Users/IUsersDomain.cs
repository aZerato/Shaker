using System.Collections.Generic;
using System.Threading.Tasks;

namespace shaker.domain.Users
{
    public interface IUsersDomain
    {
        Task<UserDto> Create(UserDto dto);

        Task<bool> Delete(int id);

        Task<UserDto> Get(int id);

        Task<bool> UserExists(UserDto dto);

        Task<IEnumerable<UserDto>> GetAll();
    }
}
