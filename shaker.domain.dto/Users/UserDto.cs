using System;

namespace shaker.domain.Users
{
    public class UserDto : IBaseDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ImgPath { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string Error { get; set; }
    }
}