using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.Areas.Api.Auth;
using shaker.domain.Users;

namespace shaker.Areas.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsersDomain _usersDomain;
        private readonly IJwtAuth _jwtAuth;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IUsersDomain usersDomain,
            IJwtAuth jwtAuth,
            ILogger<AuthController> logger
            )
        {
            _usersDomain = usersDomain;
            _jwtAuth = jwtAuth;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody]AuthDto dto)
        {
            UserDto userDto = _usersDomain.IsAuthenticated(dto);

            if (userDto == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            userDto = _jwtAuth.GenerateToken(userDto);

            return Ok(userDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody]AuthDto dto)
        {
            UserDto userDto = _usersDomain.Create(dto);

            if (userDto == null)
                return BadRequest(new { message = "An issue occured ! :(" });

            userDto = _jwtAuth.GenerateToken(userDto);

            return Ok(userDto);
        }
    }
}
