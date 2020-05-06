using LiteDB;
using shaker.data.core;

namespace shaker.data.entity.Movements
{
    public class MovementBodyZone : IBaseEntity
    {
        public MovementBodyZone()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        [BsonId]
        public string Id { get; set; }

        [BsonRef("Movement")]
        public Movement Movement { get; set; }

        [BsonRef("BodyZone")]
        public BodyZone BodyZone { get; set; }
    }
}