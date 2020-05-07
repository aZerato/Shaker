using System;
using System.Collections.Generic;
using System.Linq;
using shaker.data;
using shaker.data.entity.Posts;
using shaker.domain.dto.Posts;

namespace shaker.domain.Posts
{
    public class PostsDomain : IPostsDomain
    {
        private IUnitOfWork _uow;

        public PostsDomain(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public PostDto Create(PostDto postDto)
        {
            Post postEntity = new Post() {
                Content = postDto.Content,
                Description = postDto.Description,
                Creation = DateTime.UtcNow.Date
            };

            postDto.Id = _uow.Posts.Add(postEntity);
            _uow.Commit();

            return postDto;
        }

        public void Delete(string id)
        {
            Post post = _uow.Posts.Get(id);
            _uow.Posts.Remove(post);
        }

        public IEnumerable<PostDto> GetAll()
        {
            IEnumerable<PostDto> postDtos = _uow.Posts.GetAll().Select(s => new PostDto
            {
                Id = s.Id,
                Content = s.Content,
                Description = s.Description,
                Creation = s.Creation
            });

            return postDtos;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _uow.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
