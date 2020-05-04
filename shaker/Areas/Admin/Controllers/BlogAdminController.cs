using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Posts;
using shaker.domain.dto;
using shaker.Models.Posts;
using System;
using Microsoft.AspNetCore.Authorization;

namespace shaker.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("~/admin/blog")]
    public class BlogAdminController : Controller
    {
        private readonly IPostsDomain _postsDomain;
        private readonly ILogger<BlogAdminController> _logger;

        public BlogAdminController(
            IPostsDomain postsDomain,
            ILogger<BlogAdminController> logger)
        {
            _logger = logger;
            _postsDomain = postsDomain;
        }

        [HttpGet]
        [Route("~/admin/blog")]
        public IActionResult Index()
        {
            IList<PostListModel> list = new List<PostListModel>();

            try
            {
                list = _postsDomain.GetAll().Select(s => new PostListModel
                {
                    Id = s.Id,
                    Content = s.Content,
                    Description = s.Description,
                    Creation = s.Creation
                }).ToList();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                _logger.LogError(ex.Message);
            }

            return View(list);
        }

        [Route("~/admin/blog/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("~/admin/blog/create")]
        public IActionResult Create(PostCreateModel post)
        {
            try
            {
                _postsDomain.Create(new PostDto
                {
                    Content = post.Content,
                    Description = post.Description
                });

                return new RedirectToActionResult("Index", "Blog", null);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                _logger.LogError(ex.Message);
            }

            return View();
        }

        [HttpDelete]
        [Route("~/admin/blog/delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _postsDomain.Delete(id);

                return new RedirectToActionResult("Index", "Blog", null);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                _logger.LogError(ex.Message);
            }

            return View();
        }
    }
}