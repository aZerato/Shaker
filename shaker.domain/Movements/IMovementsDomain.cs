using System;
using System.Collections.Generic;
using shaker.domain.dto.Movements;

namespace shaker.domain.Movements
{
    public interface IMovementsDomain : IDisposable
    {
        MovementDto Create(MovementDto dto);

        BodyPartDto Create(BodyPartDto dto);

        BodyZoneDto Create(BodyZoneDto dto);

        bool Delete(string id);

        IEnumerable<MovementDto> GetAll();

        IEnumerable<MovementDto> GetAllOfType(string typeId);

        IEnumerable<MovementDto> GetAllOfBodyZone(string zoneId);

        IEnumerable<MovementDto> GetAllOfBodyPart(string partId);
    }
}
