using Microsoft.AspNetCore.Mvc;
using Restino.App.Contracts;
using Restino.App.Services;
using Restino.App.ViewModels;

namespace Restino.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDataService _userDataService;
        private readonly IAccommodationDataService _accommodationDataService;
        private readonly IReservationDataService _reservationDataService;
        public AccountController(IAuthenticationService authenticationService, IAccommodationDataService accommodationDataService, IUserDataService userDataService, IReservationDataService reservationDataService)
        {
            _authenticationService = authenticationService;
            _accommodationDataService = accommodationDataService;
            _userDataService = userDataService;
            _reservationDataService = reservationDataService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string firstName, string lastName, string userName, string email, string password)
        {
            var register = await _authenticationService.Register(firstName, lastName, userName, email, password);
            TempData["Message"] = HandleResponse<bool>(register);
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var login = await _authenticationService.Authenticate(email, password);
            TempData["Message"] = HandleResponse<LoginRequest>(login);

            if(login.Data == null)
            {
                return View();
            }

            if (login.Data.TwoFactorRequired)
            {
                return RedirectToAction("VerifyTwoFactor", new { email = email });
            }

            if (login.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult VerifyTwoFactor(string email)
        {
            var model = new VerifyTwoFactorCodeResponse { Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyTwoFactor(string email, string twoFactorCode)
        {
            var login = await _authenticationService.AuthenticateTwoFactor(email, twoFactorCode);
            TempData["Message"] = HandleResponse<LoginRequest>(login);

            if (login.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //TODO: Refactor
        [HttpGet]
        public async Task<IActionResult> Overview()
        {
            var accommodations = await _accommodationDataService.GetAllUserAccommodations();
            //var reservations = await _reservationDataService.GetUserReservation(accountId);
            TempData["Accommodations"] = accommodations;
            //TempData["Reservations"] = reservations;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return Redirect("/Home");
        }

        private string HandleResponse<T>(ApiResponse<T> response, string successMessage = "")
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
