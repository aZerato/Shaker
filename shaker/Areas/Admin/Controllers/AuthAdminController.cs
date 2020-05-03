using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using shaker.data.entity.Users;
using shaker.domain.dto.Users;

namespace shaker.Areas.Api.Controllers
{
    [Area("admin")]
    [Route("admin/auth/[action]")]
    public class AuthAdminController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthAdminController> _logger;

        public const string CriticalLogErrorMessage = "AuthAdminController::Login::Critical => User {0} :: {1}";
        public const string LockoutLogErrorMessage = "AuthAdminController::Login::Lockout => User {0}";
        public const string NotAllowedLogErrorMessage = "AuthAdminController::Login::NotAllowed => User {0}";
        public const string RequiresTwoFactorLogErrorMessage = "AuthAdminController::Login::RequiresTwoFactor => User {0}";

        public const string TempDataModelStateKey = "TempDataModelState";

        public const string TempDataErrorMessageKey = "TempDataErrorMessage";
        public const string LockoutErrorMessage = "Oops, you lock your account please contact admin !";
        public const string NotAllowedErrorMessage = "Oops, you lock your account please contact admin !";
        public const string RequiresTwoFactorErrorMessage = "Oops, you require two factor authentication !";
        public const string DefaultErrorMessage = "Oops, we encountered an error. Please try again !";

        public AuthAdminController(
            SignInManager<User> signInManager,
            ILogger<AuthAdminController> logger
            )
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ModelStateDictionary loginModelState = TempData[TempDataModelStateKey] as ModelStateDictionary;
            if (loginModelState != null)
            {
                ModelState.Merge(loginModelState);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody]AuthDto dto)
        {
            try
            {
                _logger.LogTrace($"Autentication Admin attempts for {dto.UserName}");

                if (!ModelState.IsValid)
                {
                    TempData.Add(TempDataModelStateKey, ModelState);
                    return RedirectToAction("Login");
                }

                Microsoft.AspNetCore.Identity.SignInResult result = _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, true).Result;

                if (result.IsLockedOut)
                {
                    // todo trace ip
                    _logger.LogInformation(string.Format(LockoutLogErrorMessage, dto.UserName));
                    TempData.Add(TempDataErrorMessageKey, LockoutErrorMessage);
                    return RedirectToAction("Login");
                }

                if (result.IsNotAllowed)
                {
                    // todo trace ip
                    _logger.LogInformation(string.Format(NotAllowedLogErrorMessage, dto.UserName));
                    TempData.Add(TempDataErrorMessageKey, NotAllowedErrorMessage);
                    return RedirectToAction("Login");
                }

                if (result.RequiresTwoFactor)
                {
                    // todo trace ip
                    _logger.LogInformation(string.Format(RequiresTwoFactorLogErrorMessage, dto.UserName));
                    TempData.Add(TempDataErrorMessageKey, RequiresTwoFactorErrorMessage);
                    return RedirectToAction("Login");
                }

                return RedirectToAction("Index", "BlogAdmin");
            }
            catch(Exception ex)
            {
                _logger.LogInformation(string.Format(CriticalLogErrorMessage, dto.UserName, ex.Message));
                TempData.Add(TempDataErrorMessageKey, DefaultErrorMessage);
                return RedirectToAction("Login");
            }
        }
    }
}
