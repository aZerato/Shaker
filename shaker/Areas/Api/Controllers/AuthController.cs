using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.Areas.Api.Auth;
using shaker.crosscutting.Exceptions;
using shaker.domain.Users;
using shaker.domain.dto.Users;
using shaker.crosscutting.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace shaker.Areas.Api.Controllers
{
    public class AuthController : Controller, IDisposable
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Logout()
        {
            try
            {
                _usersDomain.Logout();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return BadRequest(new { message = MessagesGetter.Get(ErrorPresentationMessages.DefaultErrorMessage) });
            }
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            _usersDomain.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
