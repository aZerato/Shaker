using System;
using shaker.data.core;

namespace shaker.domain.dto.Planning
{
    public class CalendarEventTypeDto : IBaseDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Error { get; set; }
    }
}
