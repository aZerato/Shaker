using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Users;

namespace shaker.Areas.UsersArea.Controllers
{
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

        [HttpPost]
        public UserDto Post([FromBody]AuthDto dto)
        {
            return _usersDomain.Create(dto);
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
