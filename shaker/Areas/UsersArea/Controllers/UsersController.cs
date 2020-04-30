using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            var u = new UserDto();
            u.Email = "testemail";
            u.Firstname = "testfirstname";
            u.Name = "testname";
            u.Password = "test";
            var tsk = _usersDomain.GetAll();

            await Task.WhenAll(_usersDomain.Create(u), tsk);

            return await tsk;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id)
        {
            return await _usersDomain.Get(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<UserDto> Post([FromBody]UserDto dto)
        {
            return await _usersDomain.Create(dto);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
