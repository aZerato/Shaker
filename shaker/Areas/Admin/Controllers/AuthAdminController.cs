using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using shaker.data.entity.Users;
using shaker.domain.dto.Users;

namespace shaker.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("~/admin/auth")]
    public class AuthAdminController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthAdminController> _logger;

        public static string CriticalLogErrorMessage = "AuthAdminController::Login::Critical => User {0} :: {1}";
        public static string FailedSignInLogErrorMessage = "AuthAdminController::Login::FailedSignIn => User {0}";
        public static string LockoutLogErrorMessage = "AuthAdminController::Login::Lockout => User {0}";
        public static string NotAllowedLogErrorMessage = "AuthAdminController::Login::NotAllowed => User {0}";
        public static string RequiresTwoFactorLogErrorMessage = "AuthAdminController::Login::RequiresTwoFactor => User {0}";

        public static string TempDataModelStateKey = "TempDataModelState";

        public static string TempDataErrorMessageKey = "TempDataErrorMessage";

        public static string FailedSignInErrorMessage = "Oops, failed signin !";
        public static string LockoutErrorMessage = "Oops, you lock your account please contact admin !";
        public static string NotAllowedErrorMessage = "Oops, you lock your account please contact admin !";
        public static string RequiresTwoFactorErrorMessage = "Oops, you require two factor authentication !";
        public static string DefaultErrorMessage = "Oops, we encountered an error. Please try again !";

        public AuthAdminController(
            SignInManager<User> signInManager,
            ILogger<AuthAdminController> logger
            )
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("~/admin/auth")]
        public IActionResult Index()
        {
            ModelStateDictionary loginModelState = TempData[TempDataModelStateKey] as ModelStateDictionary;
            if (loginModelState != null)
            {
                ModelState.Merge(loginModelState);
            }

            return View();
        }

        [HttpPost]
        [Route("~/admin/auth")]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AuthDto dto)
        {
            try
            {
                _logger.LogTrace($"Autentication Admin attempts for {dto.UserName}");

                if (!ModelState.IsValid)
                {
                    TempData.Add(TempDataModelStateKey, ModelState);
                    return RedirectToAction("Index");
                }

                Microsoft.AspNetCore.Identity.SignInResult result = _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, true).Result;

                if (result.IsLockedOut)
                {
                    // todo trace ip
                    _logger.LogInformation(string.Format(LockoutLogErrorMessage, dto.UserName));
                    TempData.Add(TempDataErrorMessageKey, LockoutErrorMessage);
                    return RedirectToAction("Index");
                }

                if (result.IsNotAllowed)
                {
                    // todo trace ip
                    _logger.LogInformation(string.Format(NotAllowedLogErrorMessage, dto.UserName));
                    TempData.Add(TempDataErrorMessageKey, NotAllowedErrorMessage);
                    return RedirectToAction("Index");
                }

                if (result.RequiresTwoFactor)
                {
                    // todo trace ip
                    _logger.LogInformation(string.Format(RequiresTwoFactorLogErrorMessage, dto.UserName));
                    TempData.Add(TempDataErrorMessageKey, RequiresTwoFactorErrorMessage);
                    return RedirectToAction("Index");
                }

                if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
                {
                    // todo trace ip
                    _logger.LogInformation(string.Format(FailedSignInLogErrorMessage, dto.UserName));
                    TempData.Add(TempDataErrorMessageKey, FailedSignInErrorMessage);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "BlogAdmin");
            }
            catch(Exception ex)
            {
                _logger.LogInformation(string.Format(CriticalLogErrorMessage, dto.UserName, ex.Message));
                TempData.Add(TempDataErrorMessageKey, DefaultErrorMessage);
                return RedirectToAction("Index");
            }
        }
    }
}
