using System;
using shaker.data.core;

namespace shaker.data.entity.Users
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime LastConnection { get; set; }

        public DateTime Creation { get; set; }
    }
}