using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Planning;
using shaker.domain.dto.Planning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

namespace shaker.Areas.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public IEnumerable<CalendarEventDto> Get()
        {
            return _planningDomain.GetAllOfTheMonth();
        }

        [HttpGet]
        [Route("types")]
        public IEnumerable<CalendarEventTypeDto> GetTypes()
        {
            return _planningDomain.GetAllType();
        }

        [HttpGet("from/{from}/{to?}/{type?}")]
        public IEnumerable<CalendarEventDto> Get(DateTime from, DateTime? to, string eventTypeId)
        {
            return _planningDomain.GetAll(from, to, eventTypeId);
        }

        [HttpGet("{id}")]
        public CalendarEventDto Get(string id)
        {
            return _planningDomain.Get(id);
        }

        [HttpPost]
        public void Post([FromBody]CalendarEventDto dto)
        {
            _planningDomain.Create(dto);
        }


        [HttpPut("{id}")]
        public void Put(string id, [FromBody]CalendarEventDto value)
        {
        }

        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _planningDomain.Delete(id);
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            _planningDomain.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
