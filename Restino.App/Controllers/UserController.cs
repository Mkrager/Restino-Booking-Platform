using Microsoft.AspNetCore.Mvc;
using Restino.App.Contracts;
using Restino.App.Helpers;

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
    }
}
