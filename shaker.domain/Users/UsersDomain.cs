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
            string hashedPwd = _passwordHasher.HashPassword(dto, dto.Password);

            User user = _repository
                .GetAll(u => u.UserName == dto.UserName && u.PasswordHash == hashedPwd)
                .SingleOrDefault();

            return ToUserDto(user);
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
                LastConnection = DateTime.UtcNow,
                Creation = DateTime.UtcNow
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
