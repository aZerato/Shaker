using System;
using Microsoft.AspNetCore.Identity;
using shaker.data.core;

namespace shaker.data.entity.Users
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public string Firstname { get; set; }

        public string Name { get; set; }

        public DateTime LastConnection { get; set; }

        public DateTime Creation { get; set; }
    }
}