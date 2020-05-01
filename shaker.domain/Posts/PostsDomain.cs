using System;
using System.Collections.Generic;
using System.Linq;
using shaker.data.core;
using shaker.data.entity;
using shaker.domain.dto;

namespace shaker.domain.Posts
{
    public class PostsDomain : IPostsDomain
    {
        private IRepository<Post> _postsRepository;

        public PostsDomain(IRepository<Post> postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public PostDto Create(PostDto postDto)
        {
            Post postEntity = new Post() {
                Content = postDto.Content,
                Description = postDto.Description,
                Creation = DateTime.UtcNow.Date
            };

            postDto.Id = _postsRepository.Add(postEntity);

            return postDto;
        }

        public void Delete(int id)
        {
            Post post = _postsRepository.Get(id);
            _postsRepository.Remove(post);
        }

        public IEnumerable<PostDto> GetAll()
        {
            IEnumerable<PostDto> postDtos = _postsRepository.GetAll().Select(s => new PostDto
            {
                Id = s.Id,
                Content = s.Content,
                Description = s.Description,
                Creation = s.Creation
            });

            return postDtos;
        }
    }
}
