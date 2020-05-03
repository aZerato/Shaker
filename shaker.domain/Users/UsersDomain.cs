using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.domain.Users
{
    public class UsersDomain : IUsersDomain
    {
        private readonly UserStore _userStore;
        private readonly IRepository<User> _usersRepository;
        private readonly IPasswordHasher<AuthDto> _passwordHasher;

        public UsersDomain(
            IUserStore<User> userStore,
            IRepository<User> usersRepository,
            IPasswordHasher<AuthDto> passwordHasher)
        {
            _userStore = (UserStore)userStore;
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public UserDto IsAuthenticated(AuthDto dto)
        {
            CancellationToken cancelToken = new CancellationToken();

            User matchingUser = _userStore.FindByNameAsync(dto.UserName.ToUpperInvariant(), cancelToken).Result;

            if (matchingUser == null) return null; // user not exists

            PasswordVerificationResult pwdResult = _passwordHasher.VerifyHashedPassword(dto, matchingUser.PasswordHash, dto.Password);

            if (pwdResult == PasswordVerificationResult.Failed)
            {
                _userStore.IncrementAccessFailedCountAsync(matchingUser, cancelToken);

                // manage lockout.

                return null;
            }

            if (pwdResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                string hashedPwd = _passwordHasher.HashPassword(dto, dto.Password);

                _userStore.SetPasswordHashAsync(matchingUser, hashedPwd, cancelToken);
            }

            _userStore.ResetAccessFailedCountAsync(matchingUser, cancelToken);

            _userStore.UpdateAsync(matchingUser, cancelToken);

            return ToUserDto(matchingUser);
        }

        public UserDto Create(AuthDto dto)
        {
            CancellationToken cancelToken = new CancellationToken();

            string normalizedUsername = dto.UserName.ToUpperInvariant();

            User matchingUser = _userStore.FindByNameAsync(normalizedUsername, cancelToken).Result;

            if (matchingUser != null) return null; // user exists
            
            string hashedPwd = _passwordHasher.HashPassword(dto, dto.Password);

            User user = new User();
            user.UserName = dto.UserName;
            user.NormalizedUserName = normalizedUsername;
            // user.Email = dto.UserName; // + email confirmation !
            user.PasswordHash = hashedPwd;

            IdentityResult result = _userStore.CreateAsync(user, cancelToken).Result;

            UserDto userDto = null;
            if (result == IdentityResult.Success)
            {
                user = _userStore.FindByNameAsync(user.NormalizedUserName, cancelToken).Result;
                userDto = ToUserDto(user);
            }

            return userDto;
        }

        public UserDto Get(string id)
        {
            CancellationToken cancelToken = new CancellationToken();

            User user = _userStore.FindByIdAsync(id, cancelToken).Result;

            return ToUserDto(user);
        }

        public bool Delete(string id)
        {
            CancellationToken cancelToken = new CancellationToken();

            User user = _userStore.FindByIdAsync(id, cancelToken).Result;

            IdentityResult result = _userStore.DeleteAsync(user, cancelToken).Result;

            return result == IdentityResult.Success ? true : false;
        }

        public bool Update(UserDto dto)
        {
            CancellationToken cancelToken = new CancellationToken();

            User user = _userStore.FindByIdAsync(dto.Id, cancelToken).Result;

            if (user == null) return false;

            user.UserName = dto.UserName;
            user.NormalizedUserName = dto.UserName.ToUpperInvariant();
            user.Email = dto.Email;

            IdentityResult result = _userStore.UpdateAsync(user, cancelToken).Result;

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