using shaker.domain.dto.Users;

namespace shaker.Areas.Api.Auth
{
    public interface IJwtAuth
    {
        UserDto GenerateToken(UserDto user);
    }
}
