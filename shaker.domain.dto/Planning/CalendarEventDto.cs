using System;
namespace shaker.domain.dto.Planning
{
    public class CalendarEventDto : IBaseDto
    {
        public string Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }

        public CalendarEventTypeDto Type { get; set; }

        public string hexColor { get; set; }

        public bool AllDay { get; set; }

        public string Error { get; set; }
    }
}
