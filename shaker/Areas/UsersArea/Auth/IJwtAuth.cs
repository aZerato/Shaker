using shaker.domain.Users;

namespace shaker.Areas.UsersArea.Auth
{
    public interface IJwtAuth
    {
        UserDto GenerateToken(UserDto user);
    }
}
