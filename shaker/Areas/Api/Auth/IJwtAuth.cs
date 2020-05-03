using shaker.domain.Users;

namespace shaker.Areas.Api.Auth
{
    public interface IJwtAuth
    {
        UserDto GenerateToken(UserDto user);
    }
}
