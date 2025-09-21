using Microsoft.AspNetCore.Mvc;
using Restino.App.Contracts;
using Restino.App.ViewModels.TwoFactor;

namespace Restino.App.Controllers
{
    public class TwoFactorController : Controller
    {
        private readonly ITwoFactorDataService _twoFactorDataService;
        public TwoFactorController(ITwoFactorDataService twoFactorDataService)
        {
            _twoFactorDataService = twoFactorDataService;
        }
        [HttpGet]
        public IActionResult TwoFactorManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendTwoFactorCodeAuth(SendTwoFactorCodeRequest sendTwoFactorCodeRequest)
        {
            await _twoFactorDataService.SendTwoFactorCodeAsync(sendTwoFactorCodeRequest);
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> ConfirmAddingTwoFactorAuth([FromBody] AddTwoFactorRequest addTwoFactorRequest)
        {
            await _twoFactorDataService.AddTwoFactorAsync(addTwoFactorRequest);
            TempData["Message"] = "Two-factor authentication added. Please log in to your account.";
            return Json(new { redirectUrl = Url.Action("Logout", "Account") });
        }

        [HttpDelete]
        public async Task<IActionResult> ConfirmDeletingTwoFactorAuth([FromBody] DeleteTwoFactorRequest deleteTwoFactorRequest)
        {
            await _twoFactorDataService.DeleteTwoFactorAsync(deleteTwoFactorRequest);
            TempData["Message"] = "Two-factor authentication deleted. Please log in to your account.";
            return Json(new { redirectUrl = Url.Action("Logout", "Account") });
        }
    }
}