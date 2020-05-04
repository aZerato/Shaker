using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.Areas.Api.Auth;
using shaker.crosscutting.Exceptions;
using shaker.domain.Users;
using shaker.domain.dto.Users;
using shaker.crosscutting.Messages;

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
            try
            {
                _logger.LogTrace($"Autentication attempts for {dto.UserName}");

                UserDto userDto = _usersDomain.Authenticate(dto);
                    
                userDto = _jwtAuth.GenerateToken(userDto);

                return Ok(userDto);
            }
            catch(ShakerDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return BadRequest(new { message = MessagesGetter.Get(ErrorPresentationMessages.DefaultErrorMessage) });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]SignInDto dto)
        {
            try
            {
                _logger.LogError($"Creation attempts for {dto.UserName}");

                UserDto userDto = _usersDomain.Create(dto);

                userDto = _jwtAuth.GenerateToken(userDto);

                return Ok(userDto);
            }
            catch (ShakerDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return BadRequest(new { message = MessagesGetter.Get(ErrorPresentationMessages.DefaultErrorMessage) });
            }
        }
    }
}
