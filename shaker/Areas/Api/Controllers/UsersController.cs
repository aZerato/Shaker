using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Users;

namespace shaker.Areas.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersDomain _usersDomain;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUsersDomain usersDomain,
            ILogger<UsersController> logger
            )
        {
            _usersDomain = usersDomain;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return _usersDomain.GetAll();
        }

        [HttpGet("{id}")]
        public UserDto Get(int id)
        {
            return _usersDomain.Get(id);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]UserDto value)
        {
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _usersDomain.Delete(id);
        }
    }
}
