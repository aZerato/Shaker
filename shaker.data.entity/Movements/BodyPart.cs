using LiteDB;
using shaker.data.core;

namespace shaker.data.entity.Movements
{
    public class BodyPart : IBaseEntity
    {
        public BodyPart()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgPath { get; set; }
    }
}