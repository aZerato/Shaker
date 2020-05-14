using System;
using System.Collections.Generic;
using shaker.domain.dto.Movements;

namespace shaker.domain.Movements
{
    public interface IMovementsDomain : IDisposable
    {
        MovementDto Create(MovementDto dto);

        MovementDto Update(string id, MovementDto dto);

        BodyPartDto Create(BodyPartDto dto);

        BodyZoneDto Create(BodyZoneDto dto);

        bool Delete(string id);

        MovementDto Get(string id);

        IEnumerable<MovementDto> GetAll();

        IEnumerable<MovementTypeDto> GetAllTypes();

        IEnumerable<MovementDto> GetAllOfType(string typeId);

        IEnumerable<BodyZoneDto> GetAllBodyZones();

        IEnumerable<MovementDto> GetAllOfBodyZone(string zoneId);

        IEnumerable<BodyPartDto> GetAllBodyParts();

        IEnumerable<MovementDto> GetAllOfBodyPart(string partId);
    }
}
