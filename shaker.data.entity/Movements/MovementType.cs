using LiteDB;
using shaker.data.core;

namespace shaker.data.entity.Movements
{
    public class MovementType : IBaseEntity
    {
        public MovementType()
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

///
//Barbell
//  Kettlebell
//BodyWeight
// Streching
