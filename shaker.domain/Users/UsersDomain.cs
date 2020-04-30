using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.domain.Users
{
    public class UsersDomain : IUsersDomain
    {
        private IRepository<User> _repository;

        public UsersDomain(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> Create(UserDto dto)
        {
            Task<User> getTask = Task.Run(() =>
                _repository.GetAll(u => u.Email == dto.Email && u.Password == dto.Password).FirstOrDefault());

            Task<int> addTask = getTask.ContinueWith((gtsk) =>
            {
                if (getTask.IsCompletedSuccessfully && getTask.Result == null)
                {
                    User entity = new User()
                    {
                        Firstname = dto.Firstname,
                        Name = dto.Name,
                        Password = dto.Password,
                        Email = dto.Email,
                        LastConnection = DateTime.UtcNow,
                        Creation = DateTime.UtcNow
                    };
                    return _repository.Add(entity);
                }
                return 0;
            });
            
            await Task.WhenAll(getTask, addTask);

            dto.Id = addTask.IsCompletedSuccessfully ? addTask.Result : 0;

            return dto;
        }

        public async Task<UserDto> Get(int id)
        {
            Task<UserDto> getTask = Task.Run(() => {
                    User u = _repository.Get(id);
                    return ToUserDto(u);
                });

            return await getTask;
        }

        public async Task<bool> UserExists(UserDto dto)
        {
            Task<bool> getTask = Task.Run(() => {
                return _repository.GetAll(u => u.Email == dto.Email && u.Password == dto.Password).FirstOrDefault() != null;
            });

            return await getTask;
        }

        public async Task<bool> Delete(int id)
        {
            Task<User> getTask = Task.Run(() => _repository.Get(id));

            Task<bool> removeTsk = getTask.ContinueWith(gTsk =>
            {
                if (getTask.IsCompletedSuccessfully)
                {
                    return _repository.Remove(getTask.Result);
                }
                return false;
            });

            return await removeTsk;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            Task<IEnumerable<UserDto>> getAllTask =
                Task.Run(() =>
                {
                    return  _repository.GetAll(
                                ToUserDtoSb()); 
                });

            return await getAllTask;
        }

        private static Expression<Func<User, UserDto>> ToUserDtoSb()
        {
            return u => new UserDto
            {
                Id = u.Id,
                Firstname = u.Firstname,
                Name = u.Name,
                Email = u.Email,
                Creation = u.Creation,
                LastConnection = u.LastConnection
            };
        }

        private static UserDto ToUserDto(User u)
        {
            return u == null ? null :
                new UserDto
                {
                    Id = u.Id,
                    Firstname = u.Firstname,
                    Name = u.Name,
                    Email = u.Email,
                    Creation = u.Creation,
                    LastConnection = u.LastConnection
                };
        }
    }
}
