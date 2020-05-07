using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using shaker.crosscutting.Accessors;
using shaker.crosscutting.Exceptions;
using shaker.data;
using shaker.data.entity.Planning;
using shaker.data.entity.Users;
using shaker.domain.dto.Planning;
using shaker.crosscutting.Extensions;

namespace shaker.domain.Planning
{
    public class PlanningDomain : IPlanningDomain
    {
        private IUnitOfWork _uow;
        private IConnectedUserAccessor _connectedUserAccessor;

        public PlanningDomain(
            IUnitOfWork uow,
            IConnectedUserAccessor connectedUserAccessor)
        {
            _uow = uow;
            _connectedUserAccessor = connectedUserAccessor;
        }

        public CalendarEventDto Create(CalendarEventDto dto)
        {
            CalendarEvent entity = new CalendarEvent()
            {
                AllDay = dto.AllDay,
                Start = dto.Start,
                End = dto.End,
                User = new User() { Id = _connectedUserAccessor.GetId() },
                hexColor = dto.hexColor,
                Title = dto.Title,
                Type = new CalendarEventType() { Id = dto.Id }
            };

            _uow.CalendarEvents.Add(entity);
            _uow.Commit();

            dto = ToCalendarEventDto(entity);

            return dto;
        }

        public CalendarEventTypeDto Create(CalendarEventTypeDto dto)
        {
            CalendarEventType entity = new CalendarEventType()
            {
                Title = dto.Title
            };

            _uow.CalendarEventTypes.Add(entity);
            _uow.Commit();

            dto = ToCalendarEventTypeDto(entity);

            return dto;
        }


        public CalendarEventDto Get(string id)
        {
            CalendarEvent entity = _uow.CalendarEvents.Get(id, x => x.Type);

            if (entity == null)
                throw new ShakerDomainException("No entry in DB");

            return ToCalendarEventDto(entity);
        }

        public bool Delete(string id)
        {
            CalendarEvent entity = _uow.CalendarEvents.Get(id);

            if (entity == null)
                throw new ShakerDomainException("No entry in DB");

            return _uow.CalendarEvents.Remove(entity);
        }

        public IEnumerable<CalendarEventDto> GetAllOfTheMonth()
        {
            return GetAll(DateTime.Now.FirstDayOfMonth(), DateTime.Now.LastDayOfMonth(), null);
        }

        public IEnumerable<CalendarEventDto> GetAll(DateTime from, DateTime? to, string eventTypeId = null)
        {
            IEnumerable<CalendarEventDto> calendarEvents =
                _uow.CalendarEvents.GetAll(
                    ToCalendarEventDtoSb(),
                    x => x.Start >= from,
                    x => x.Type);

            if (to.HasValue)
                calendarEvents = calendarEvents.Where(c => c.End <= to);

            if (!string.IsNullOrEmpty(eventTypeId))
                calendarEvents = calendarEvents.Where(c => c.Type.Id == eventTypeId);

            return calendarEvents;
        }

        public IEnumerable<CalendarEventTypeDto> GetAllType()
        {
            return _uow.CalendarEventTypes.GetAll(ToCalendarEventTypeDtoSb());
        }

        private static Expression<Func<CalendarEvent, CalendarEventDto>> ToCalendarEventDtoSb()
        {
            return entity => new CalendarEventDto
            {
                Id = entity.Id,
                Title = entity.Title,
                UserId = entity.User.Id,
                AllDay = entity.AllDay,
                Start = entity.Start,
                End = entity.End,
                hexColor = entity.hexColor,
                Type = new CalendarEventTypeDto
                {
                    Id = entity.Type.Id,
                    Title = entity.Type.Title
                }
            };
        }

        private static CalendarEventDto ToCalendarEventDto(CalendarEvent entity)
        {
            return new CalendarEventDto
            {
                Id = entity.Id,
                Title = entity.Title,
                UserId = entity.User.Id,
                AllDay = entity.AllDay,
                Start = entity.Start,
                End = entity.End,
                hexColor = entity.hexColor,
                Type = entity.Type != null ? ToCalendarEventTypeDto(entity.Type) : null
            };
        }

        private static Expression<Func<CalendarEventType, CalendarEventTypeDto>> ToCalendarEventTypeDtoSb()
        {
            return entity => new CalendarEventTypeDto
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        private static CalendarEventTypeDto ToCalendarEventTypeDto(CalendarEventType entity)
        {
            return new CalendarEventTypeDto
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _uow.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
