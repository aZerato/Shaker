﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using shaker.crosscutting.Exceptions;
using shaker.data;
using shaker.data.entity.Movements;
using shaker.domain.dto.Movements;

namespace shaker.domain.Movements
{
    public class MovementsDomain : IMovementsDomain
    {
        private IUnitOfWork _uow;

        public MovementsDomain(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public MovementDto Create(MovementDto dto)
        {
            Movement entity = new Movement();
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.ImgPath = dto.ImgPath;
            entity.MovementType = new MovementType() { Id = dto.MovementType.Id };

            string id = _uow.Movements.Add(entity);
            if (string.IsNullOrEmpty(id))
                _uow.RollbackChanges();
            else
                _uow.Commit();

            return ToMovementDto(entity);
        }

        public MovementTypeDto Create(MovementTypeDto dto)
        {
            MovementType entity = new MovementType();
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.ImgPath = dto.ImgPath;

            string id = _uow.MovementTypes.Add(entity);
            if (string.IsNullOrEmpty(id))
                _uow.RollbackChanges();
            else
                _uow.Commit();

            return ToMovementTypeDto(entity);
        }

        public BodyPartDto Create(BodyPartDto dto)
        {
            BodyPart entity = new BodyPart();
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.ImgPath = dto.ImgPath;

            string id = _uow.BodyParts.Add(entity);
            if (string.IsNullOrEmpty(id))
                _uow.RollbackChanges();
            else
                _uow.Commit();

            return ToBodyPartDto(entity);
        }

        public BodyZoneDto Create(BodyZoneDto dto)
        {
            BodyZone entity = new BodyZone();
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.ImgPath = dto.ImgPath;

            string id = _uow.BodyZones.Add(entity);
            if (string.IsNullOrEmpty(id))
                _uow.RollbackChanges();
            else
                _uow.Commit();

            return ToBodyZoneDto(entity);
        }

        public bool Delete(string movementId)
        {
            Movement entity = _uow.Movements.Get(movementId);

            if (entity == null)
                throw new ShakerDomainException("No entry in DB");

            bool state = _uow.Movements.Remove(entity);
            if (state)
                _uow.RollbackChanges();
            else
                _uow.Commit();

            return state;
        }

        public IEnumerable<MovementDto> GetAll()
        {
            return _uow.Movements.GetAll(ToMovementDtoSb());
        }

        public IEnumerable<MovementDto> GetAllOfType(string typeId)
        {
            return _uow.Movements.GetAll(ToMovementDtoSb(), m => m.MovementType.Id == typeId);
        }

        public IEnumerable<MovementDto> GetAllOfBodyZone(string bodyZoneId)
        {
            return _uow.MovementBodyZone.GetAll(BodyZoneToMovementDtoSb(), m => m.BodyZone.Id == bodyZoneId);
        }

        public IEnumerable<MovementDto> GetAllOfBodyPart(string bodyPartId)
        {
            return _uow.MovementBodyPart.GetAll(BodyPartToMovementDtoSb(), m => m.BodyPart.Id == bodyPartId);
        }

        public static BodyPartDto ToBodyPartDto(BodyPart entity)
        {
            return new BodyPartDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImgPath = entity.ImgPath
            };
        }

        public static BodyZoneDto ToBodyZoneDto(BodyZone entity)
        {
            return new BodyZoneDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImgPath = entity.ImgPath
            };
        }

        public static MovementDto ToMovementDto(Movement entity)
        {
            return new MovementDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImgPath = entity.ImgPath,
                MovementType = ToMovementTypeDto(entity.MovementType)
            };
        }

        public static MovementTypeDto ToMovementTypeDto(MovementType entity)
        {
            return new MovementTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImgPath = entity.ImgPath
            };
        }

        public static Expression<Func<Movement, MovementDto>> ToMovementDtoSb()
        {
            return m => new MovementDto
            {
                Id = m.Id,
                Description = m.Description,
                ImgPath = m.ImgPath,
                Name = m.Name,
                MovementType = ToMovementTypeDto(m.MovementType)
            };
        }

        public static Expression<Func<MovementBodyZone, MovementDto>> BodyZoneToMovementDtoSb()
        {
            return mb => new MovementDto
            {
                Id = mb.Movement.Id,
                Description = mb.Movement.Description,
                ImgPath = mb.Movement.ImgPath,
                Name = mb.Movement.Name,
                MovementType = ToMovementTypeDto(mb.Movement.MovementType)
            };
        }

        public static Expression<Func<MovementBodyPart, MovementDto>> BodyPartToMovementDtoSb()
        {
            return mb => new MovementDto
            {
                Id = mb.Movement.Id,
                Description = mb.Movement.Description,
                ImgPath = mb.Movement.ImgPath,
                Name = mb.Movement.Name,
                MovementType = ToMovementTypeDto(mb.Movement.MovementType)
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
