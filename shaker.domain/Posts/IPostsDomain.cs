using System.Collections.Generic;
using shaker.domain.dto;

namespace shaker.domain.Posts
{
    public interface IPostsDomain
    {
        PostDto Create(PostDto post);

        void Delete(int id);

        IEnumerable<PostDto> GetAll();
    }
}
