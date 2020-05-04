using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shaker.domain.Users;

namespace shaker.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    [Route("~/admin/users/{action}")]
    public class UsersAdminController : Controller
    {
        private readonly IUsersDomain _usersDomain;
        private readonly ILogger<UsersAdminController> _logger;

        public UsersAdminController(
            IUsersDomain usersDomain,
            ILogger<UsersAdminController> logger
            )
        {
            _usersDomain = usersDomain;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_usersDomain.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            return View(_usersDomain.Get(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if(!_usersDomain.Delete(id))
            { 
                return RedirectToAction("Details", id);
            }

            return RedirectToAction("Index");
        }
    }
}
