using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.domain.Users
{
    public class UsersDomain : IUsersDomain
    {
        private IRepository<User> _repository;
        private readonly IPasswordHasher<AuthDto> _passwordHasher;

        public UsersDomain(
            IRepository<User> repository,
            IPasswordHasher<AuthDto> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public UserDto IsAuthenticated(AuthDto dto)
        {
            User matchingUser = _repository.GetAll(u => u.UserName == dto.UserName).FirstOrDefault();

            PasswordVerificationResult pwdResult = _passwordHasher.VerifyHashedPassword(dto, matchingUser.PasswordHash, dto.Password);

            if (pwdResult == PasswordVerificationResult.Failed)
            {
                matchingUser.AccessFailedCount++;

                _repository.Update(matchingUser);

                return null;
            }

            if (pwdResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                string hashedPwd = _passwordHasher.HashPassword(dto, dto.Password);

                matchingUser.PasswordHash = hashedPwd;
                
            }

            matchingUser.LastConnection = DateTime.UtcNow.Date;

            _repository.Update(matchingUser);

            return ToUserDto(matchingUser);
        }

        public UserDto Create(AuthDto dto)
        {
            User userNameExists = _repository.GetAll(u => u.UserName == dto.UserName).FirstOrDefault();

            if (userNameExists != null) return null;

            string hashedPwd = _passwordHasher.HashPassword(dto, dto.Password);

            User entity = new User()
            {
                UserName = dto.UserName,
                PasswordHash = hashedPwd,
                LastConnection = DateTime.UtcNow.Date,
                Creation = DateTime.UtcNow.Date
            };

            UserDto userDto = new UserDto
            {
                Id = _repository.Add(entity),
                UserName = dto.UserName
            };

            return userDto;
        }

        public UserDto Get(int id)
        {
            User user = _repository.Get(id);

            return ToUserDto(user);
        }

        public bool Delete(int id)
        {
            User user = _repository.Get(id);

            if (user == null) return false;

            return _repository.Remove(user);
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _repository.GetAll(ToUserDtoSb()); 
        }

        private static Expression<Func<User, UserDto>> ToUserDtoSb()
        {
            return u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Firstname = u.Firstname,
                Name = u.Name,
                Email = u.Email,
                Creation = u.Creation
            };
        }

        private static UserDto ToUserDto(User u)
        {
            return u == null ? null :
                new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Firstname = u.Firstname,
                    Name = u.Name,
                    Email = u.Email,
                    Creation = u.Creation
                };
        }
    }
}
