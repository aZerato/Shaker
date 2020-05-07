using System;
using System.Collections.Generic;
using shaker.domain.dto.Posts;

namespace shaker.domain.Posts
{
    public interface IPostsDomain : IDisposable
    {
        PostDto Create(PostDto post);

        void Delete(string id);

        IEnumerable<PostDto> GetAll();
    }
}
