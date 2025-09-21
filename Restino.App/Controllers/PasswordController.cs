using Microsoft.AspNetCore.Mvc;
using Restino.App.Contracts;
using Restino.App.Helpers;
using Restino.App.ViewModels.ResetPassword;

namespace Restino.App.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IPasswordDataService _passwordDataService;
        public PasswordController(IPasswordDataService passwordDataService)
        {
            _passwordDataService = passwordDataService;
        }

        [HttpGet]
        public IActionResult SendPasswordResetCode()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendPasswordResetCode(SendPasswordResetCodeRequest sendPasswordResetCodeRequest)
        {
            var result = await _passwordDataService.SendPasswordResetCodeAsync(sendPasswordResetCodeRequest);

            if (result.IsSuccess)
            {
                return RedirectToAction("ChangePassword");
            }

            TempData["Message"] = HandleErrors.HandleResponse(result);
            return View();
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ResetPasswordRequest());
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return Json(new { errors });
            }

            var result = await _passwordDataService.ResetPasswordAsync(resetPasswordRequest);
            TempData["Message"] = HandleErrors.HandleResponse(result);
            return Json(new { redirectToUrl = Url.Action("Index", "Home") });
        }
    }
}