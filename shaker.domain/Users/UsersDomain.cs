using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using shaker.data.core;
using shaker.data.entity.Users;
using shaker.crosscutting.Exceptions;
using shaker.domain.dto.Users;

namespace shaker.domain.Users
{
    public class UsersDomain : IUsersDomain
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _usersRepository;

        public UsersDomain(
            UserManager<User> userManager,
            IRepository<User> usersRepository)
        {
            _userManager = userManager;
            _usersRepository = usersRepository;
        }

        public UserDto IsAuthenticated(AuthDto dto)
        {
            User matchingUser = _userManager.FindByNameAsync(dto.UserName.ToUpperInvariant()).Result;

            if (matchingUser == null)
            {
                throw new DomainException("User doesn't exists");
            }

            bool pwdResult = _userManager.CheckPasswordAsync(matchingUser, dto.Password).Result;

            if (!pwdResult)
            {
                _userManager.AccessFailedAsync(matchingUser);

                // manage lockout.

                throw new DomainException("Invalid combinaison");
            }

            _userManager.ResetAccessFailedCountAsync(matchingUser);

            _userManager.UpdateAsync(matchingUser);

            return ToUserDto(matchingUser);
        }

        public UserDto Create(AuthDto dto)
        {
            User matchingUser = _userManager.FindByNameAsync(dto.UserName).Result;

            if (matchingUser != null)
            {
                throw new DomainException("User already exists");
            }

            User user = new User();
            user.UserName = dto.UserName;
            // user.Email = dto.UserName; // + email confirmation !

            IdentityResult result = _userManager.CreateAsync(user, dto.Password).Result;

            // messages password validation
            if (result != IdentityResult.Success)
            {
                string errors = string.Empty;

                foreach(var er in result.Errors)
                {
                    errors += $" {er.Description}";
                }

                throw new DomainException(errors);
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