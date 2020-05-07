using LiteDB;
using shaker.data.core;

namespace shaker.data.entity.Movements
{
    public class MovementBodyPart : IBaseEntity
    {
        public MovementBodyPart()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        [BsonId]
        public string Id { get; set; }

        [BsonRef("Movement")]
        public Movement Movement { get; set; }

        [BsonRef("BodyPart")]
        public BodyPart BodyPart { get; set; }
    }
}