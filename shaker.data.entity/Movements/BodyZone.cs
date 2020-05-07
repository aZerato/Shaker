using System.Collections.Generic;
using LiteDB;
using shaker.data.core;

namespace shaker.data.entity.Movements
{
    public class BodyZone : IBaseEntity
    {
        public BodyZone()
        {
            Id = ObjectId.NewObjectId().ToString();
            BodyParts = new List<BodyPart>();
        }

        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgPath { get; set; }

        [BsonIgnore]
        public IList<BodyPart> BodyParts { get; set; }
    }
}