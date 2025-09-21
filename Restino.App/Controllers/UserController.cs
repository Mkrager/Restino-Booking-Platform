﻿using Microsoft.AspNetCore.Mvc;
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

        //TODO: refactor
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var result = await _userDataService.GetUserDetails();
            var accommodations = await _accommodationDataService.GetAllUserAccommodations();
            var reservations = await _reservationDataService.GetUserReservation();
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

        [HttpGet]
        public IActionResult TwoFactorManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendTwoFactorCodeAuthForAdd(string email)
        {
            await _userDataService.SendTwoFactorCodeAsync(email);
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> ConfirmAddingTwoFactorAuth([FromBody] TwoFactorDto model)
        {
            await _userDataService.AddTwoFactorAsync(model.Email, model.Code);
            TempData["Message"] = "Two-factor authentication added. Please log in to your account.";
            return Json(new { redirectUrl = Url.Action("Logout", "Account") });
        }

        [HttpPost]
        public async Task<IActionResult> SendTwoFactorCodeAuthForDelete(string email)
        {
            await _userDataService.SendTwoFactorCodeAsync(email);
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> ConfirmDeletingTwoFactorAuth([FromBody] TwoFactorDto model)
        {
            await _userDataService.DeleteTwoFactorAsync(model.Email, model.Code);
            TempData["Message"] = "Two-factor authentication deleted. Please log in to your account.";
            return Json(new { redirectUrl = Url.Action("Logout", "Account") });
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
