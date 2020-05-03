using LiteDB;
using Microsoft.AspNetCore.Identity;
using shaker.data.core;

namespace shaker.data.entity.Users
{
    public class Role : IdentityRole<string>, IBaseEntity
    {
        public Role()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        public Role(string roleName) : this()
        {
            Name = roleName;
        }

        [BsonId]
        public override string Id { get; set; }
    }
}