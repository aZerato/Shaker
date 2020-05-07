using System.Collections.Generic;
using LiteDB;
using shaker.data.core;

namespace shaker.data.entity.Movements
{
    public class Movement : IBaseEntity
    {
        public Movement()
        {
            Id = ObjectId.NewObjectId().ToString();
            BodyZones = new List<BodyZone>();
        }

        [BsonId]
        public string Id { get; set; }

        [BsonRef("MovementType")]
        public MovementType MovementType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgPath { get; set; }

        [BsonIgnore]
        public IList<BodyZone> BodyZones { get; set; }
    }
}