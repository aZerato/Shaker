using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using shaker.domain.Movements;
using shaker.domain.dto.Movements;

namespace shaker.Areas.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class MovementsController : Controller
    {
        private readonly IMovementsDomain _movementsDomain;
        private readonly ILogger<MovementsController> _logger;

        public MovementsController(
            IMovementsDomain movementsDomain,
            ILogger<MovementsController> logger
            )
        {
            _movementsDomain = movementsDomain;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<MovementDto> Get()
        {
            return _movementsDomain.GetAll();
        }

        [HttpGet("{id}")]
        public MovementDto Get(string id)
        {
            return _movementsDomain.Get(id);
        }

        [HttpGet]
        [Route("types/{id}")]
        public IEnumerable<MovementDto> GetAllOfTypes(string id)
        {
            return _movementsDomain.GetAllOfType(id);
        }

        [HttpGet]
        [Route("types")]
        public IEnumerable<MovementTypeDto> GetAllTypes()
        {
            return _movementsDomain.GetAllTypes();
        }

        [HttpPost]
        public void Post([FromBody]MovementDto dto)
        {
            _movementsDomain.Create(dto);
        }


        [HttpPut("{id}")]
        public void Put(string id, [FromBody]MovementDto value)
        {
            _movementsDomain.Update(id, value);
        }

        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _movementsDomain.Delete(id);
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            _movementsDomain.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
