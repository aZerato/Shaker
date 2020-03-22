using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Posts;
using shaker.domain.dto;
using shaker.Models.Posts;

namespace shaker.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostsDomain _postsDomain;
        private readonly ILogger<HomeController> _logger;

        public BlogController(
            IPostsDomain postsDomain,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _postsDomain = postsDomain;
        }

        public IActionResult Index()
        {
            IList<PostListModel> list = _postsDomain.GetAll().Select(s => new PostListModel
            {
                Id = s.Id,
                Content = s.Content,
                Description = s.Description,
                Creation = s.Creation
            }).ToList();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostCreateModel post)
        {
            _postsDomain.Create(new PostDto
            {
                Content = post.Content,
                Description = post.Description
            });

            return new RedirectToActionResult("Index", "Blog", null);
        }

        public IActionResult Delete(int id)
        {
            _postsDomain.Delete(id);

            return new RedirectToActionResult("Index", "Blog", null);
        }
    }
}
