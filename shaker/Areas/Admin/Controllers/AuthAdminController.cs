using System;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using shaker.crosscutting.Exceptions;
using shaker.crosscutting.Messages;
using shaker.domain.dto.Users;
using shaker.domain.Users;

namespace shaker.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("~/admin/auth")]
    public class AuthAdminController : Controller, IDisposable
    {
        private readonly IUsersDomain _usersDomain;
        private readonly ILogger<AuthAdminController> _logger;

        public static string TempDataModelStateKey = "TempDataModelState";
        public static string TempDataErrorMessageKey = "TempDataErrorMessage";

        public AuthAdminController(
            IUsersDomain usersDomain,
            ILogger<AuthAdminController> logger
            )
        {
            _usersDomain = usersDomain;
            _logger = logger;
        }

        [HttpGet]
        [Route("~/admin/auth/{returnUrl?}")]
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;

            ModelStateDictionary loginModelState = TempData[TempDataModelStateKey] as ModelStateDictionary;
            if (loginModelState != null)
            {
                ModelState.Merge(loginModelState);
            }

            return View();
        }

        [HttpPost]
        [Route("~/admin/auth/{returnurl?}")]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AuthDto dto, string returnUrl = null)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData.Add(TempDataModelStateKey, ModelState);
                    return RedirectToAction("Index");
                }

                _usersDomain.Authenticate(dto);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = HttpUtility.UrlDecode(returnUrl);
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "BlogAdmin");
            }
            catch(ShakerDomainException ex)
            {
                TempData.Add(TempDataErrorMessageKey, ex.Message);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.Message);
                TempData.Add(TempDataErrorMessageKey, MessagesGetter.Get(ErrorPresentationMessages.DefaultErrorMessage));
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("~/admin/logout")]
        public IActionResult Logout()
        {
            try
            {
                _usersDomain.Logout();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                TempData.Add(TempDataErrorMessageKey, MessagesGetter.Get(ErrorPresentationMessages.DefaultErrorMessage));
                return RedirectToAction("Index");
            }
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            _usersDomain.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}