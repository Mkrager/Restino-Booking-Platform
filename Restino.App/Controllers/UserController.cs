using Microsoft.AspNetCore.Mvc;
using Restino.App.Contracts;
using Restino.App.Services;
using Restino.App.ViewModels;

namespace Restino.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDataService _userDataService;
        private readonly IAccommodationDataService _accommodationDataService;
        private readonly IReservationDataService _reservationDataService;

        public UserController(IUserDataService userDataService, IAccommodationDataService accommodationDataService, IReservationDataService reservationDataService)
        {
            _userDataService = userDataService;
            _accommodationDataService = accommodationDataService;
            _reservationDataService = reservationDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            var searchResult = await _userDataService.SearchUser(searchString);
            return View(searchResult);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string userId)
        {
            var result = await _userDataService.GetUserDetails(userId);
            var accommodations = await _accommodationDataService.GetAllUserAccommodations(userId);
            var reservations = await _reservationDataService.GetUserReservation(userId);
            TempData["Accommodations"] = accommodations;
            TempData["Reservations"] = reservations;
            return View(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string userId)
        {
            var response = await _userDataService.DeleteUser(userId);
            return Json(new { success = true });
        }


        [HttpGet]
        public IActionResult SendPasswordResetCode()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendPasswordResetCode(string email)
        {
            var result = await _userDataService.SendPasswordResetCodeAsync(email);

            if (result.IsSuccess)
            {
                return RedirectToAction("ChangePassword");
            }

            TempData["Message"] = HandleResponse(result);
            return View();
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordVm());
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVm model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return Json(new { errors });
            }

            var result = await _userDataService.ChangePasswordAsync(model.Email, model.NewPassword, model.Token);
            TempData["Message"] = HandleResponse(result);
            return Json(new { redirectToUrl = Url.Action("Index", "Home") });
        }


        private string HandleResponse<T>(ApiResponse<T> response, string successMessage = "Success")
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return successMessage;
            }
            else
            {
                return response.ErrorText;
            }
        }
    }
}
