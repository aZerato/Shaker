using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using shaker.data.core;
using shaker.data.entity.Users;
using shaker.crosscutting.Exceptions;
using shaker.domain.dto.Users;
using shaker.crosscutting.Messages;
using Microsoft.Extensions.Logging;

namespace shaker.domain.Users
{
    public class UsersDomain : IUsersDomain
    {
        private readonly ILogger<UsersDomain> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _usersRepository;

        public UsersDomain(
            ILogger<UsersDomain> logger,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IRepository<User> usersRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _usersRepository = usersRepository;
        }

        public UserDto Authenticate(AuthDto dto)
        {
            SignInResult result = _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, true).Result;

            if(result != SignInResult.Success)
            {
                string errorLog = string.Empty;
                string errorPresentation = string.Empty;

                if (result.IsLockedOut)
                {
                    errorLog = MessagesGetter.Get(ErrorLogMessages.LockoutLogErrorMessage, dto.UserName);
                    errorPresentation = MessagesGetter.Get(ErrorPresentationMessages.LockoutErrorMessage);
                }

                if (result.IsNotAllowed)
                {
                    errorLog = MessagesGetter.Get(ErrorLogMessages.NotAllowedLogErrorMessage, dto.UserName);
                    errorPresentation = MessagesGetter.Get(ErrorPresentationMessages.NotAllowedErrorMessage);
                }

                if (result.RequiresTwoFactor)
                {
                    errorLog = MessagesGetter.Get(ErrorLogMessages.RequiresTwoFactorLogErrorMessage, dto.UserName);
                    errorPresentation = MessagesGetter.Get(ErrorPresentationMessages.RequiresTwoFactorErrorMessage);
                }

                if (result == SignInResult.Failed)
                {
                    errorLog = MessagesGetter.Get(ErrorLogMessages.FailedSignInLogErrorMessage, dto.UserName);
                    errorPresentation = MessagesGetter.Get(ErrorPresentationMessages.FailedSignInErrorMessage);
                }

                _logger.LogError(errorLog);

                throw new ShakerDomainException(errorPresentation);
            }

            User user = _userManager.FindByNameAsync(dto.UserName.ToUpperInvariant()).Result;

            return ToUserDto(user);
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public UserDto Create(SignInDto dto)
        {
            User user = new User();
            user.UserName = dto.UserName;
            user.Email = dto.Email; // Todo + email confirmation !

            IdentityResult result = _userManager.CreateAsync(user, dto.Password).Result;

            if (result != IdentityResult.Success)
            {
                string errors = string.Empty;

                foreach(var er in result.Errors)
                {
                    errors += $" {er.Description}";
                }

                throw new ShakerDomainException(errors);
            }

            return ToUserDto(user);
        }

        public UserDto Get(string id)
        {
            User user = _userManager.FindByIdAsync(id).Result;

            return ToUserDto(user);
        }

        public bool Delete(string id)
        {
            User user = _userManager.FindByIdAsync(id).Result;

            IdentityResult result = _userManager.DeleteAsync(user).Result;

            return result == IdentityResult.Success ? true : false;
        }

        public bool Update(UserDto dto)
        {
            User user = _userManager.FindByIdAsync(dto.Id).Result;

            if (user == null) return false;

            user.UserName = dto.UserName;
            user.NormalizedUserName = dto.UserName.ToUpperInvariant();
            user.Email = dto.Email;

            IdentityResult result = _userManager.UpdateAsync(user).Result;

            return result == IdentityResult.Success ? true : false;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _usersRepository.GetAll(ToUserDtoSb()); 
        }

        private static Expression<Func<User, UserDto>> ToUserDtoSb()
        {
            return u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                ImgPath = u.ImgPath
            };
        }

        private static UserDto ToUserDto(User u)
        {
            return u == null ? null :
                new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    ImgPath = u.ImgPath
                };
        }
    }
}