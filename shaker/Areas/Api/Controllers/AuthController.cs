﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.Areas.Api.Auth;
using shaker.crosscutting.Exceptions;
using shaker.domain.Users;

namespace shaker.Areas.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsersDomain _usersDomain;
        private readonly IJwtAuth _jwtAuth;
        private readonly ILogger<AuthController> _logger;

        public const string DefaultErrorMessage = "Oops, we encountered an error. Please try again !";

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

                UserDto userDto = _usersDomain.IsAuthenticated(dto);
                    
                userDto = _jwtAuth.GenerateToken(userDto);

                return Ok(userDto);
            }
            catch(DomainException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return BadRequest(new { message = DefaultErrorMessage });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]AuthDto dto)
        {
            try
            {
                _logger.LogError($"Creation attempts for {dto.UserName}");

                UserDto userDto = _usersDomain.Create(dto);

                userDto = _jwtAuth.GenerateToken(userDto);

                return Ok(userDto);
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return BadRequest(new { message = DefaultErrorMessage });
            }
        }
    }
}
