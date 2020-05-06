using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Users;
using shaker.domain.dto.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace shaker.Areas.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PlanningController : Controller
    {
        private readonly IPlanningDomain _planningDomain;
        private readonly ILogger<PlanningController> _logger;

        public PlanningController(
            IPlanningDomain planningDomain,
            ILogger<PlanningController> logger
            )
        {
            _planningDomain = planningDomain;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return _usersDomain.GetAll();
        }

        [HttpGet("{id}")]
        public UserDto Get(string id)
        {
            return _usersDomain.Get(id);
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody]UserDto value)
        {
        }

        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _usersDomain.Delete(id);
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            _usersDomain.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
