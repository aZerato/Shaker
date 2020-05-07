using System;
using System.Collections.Generic;
using shaker.domain.dto.Planning;

namespace shaker.domain.Planning
{
    public interface IPlanningDomain : IDisposable
    {
        CalendarEventDto Create(CalendarEventDto dto);

        CalendarEventTypeDto Create(CalendarEventTypeDto dto);

        bool Delete(string id);

        CalendarEventDto Get(string eventId);

        IEnumerable<CalendarEventDto> GetAll(DateTime from, DateTime? to, string eventTypeId);

        IEnumerable<CalendarEventTypeDto> GetAllType();
    }
}
