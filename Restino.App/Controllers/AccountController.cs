using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Restino.App.Contracts;
using Restino.App.Helpers;
using Restino.App.ViewModels;
using Restino.App.ViewModels.Authenticate;

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
        public async Task<IActionResult> Register(RegistrationRequest registerRequest)
        {
            var register = await _authenticationService.Register(registerRequest);
            TempData["Message"] = HandleErrors.HandleResponse(register);
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateRequest authenticateRequest)
        {
            var login = await _authenticationService.Authenticate(authenticateRequest);
            TempData["Message"] = HandleErrors.HandleResponse(login);

            //if (login.Data.TwoFactorRequired)
            //{
            //    return RedirectToAction("VerifyTwoFactor", new { email = authenticateRequest.Email });
            //}

            if (login.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //[HttpGet]
        //public IActionResult VerifyTwoFactor(string email)
        //{
        //    var model = new VerifyTwoFactorCodeResponse { Email = email };
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> VerifyTwoFactor(string email, string twoFactorCode)
        //{
        //    var login = await _authenticationService.AuthenticateTwoFactor(email, twoFactorCode);
        //    TempData["Message"] = HandleResponse<LoginRequest>(login);

        //    if (login.IsSuccess)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View();
        //}

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
    }
}
