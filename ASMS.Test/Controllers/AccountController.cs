using ASMS.Library.Models;
using ASMS.Client.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace ASMS.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            LoginRequest loginRequest = new LoginRequest();
            return View(loginRequest);
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost(LoginRequest loginRequest)
        {
            if (!loginRequest.IsCorrectRequest)
            {
                TempData["ErrorMessage"] = loginRequest.ErrorText;
                return RedirectToAction("Login", "Account");
            }

            var response = await _accountService.Login(loginRequest);

            if (!response.IsSuccess)
            {
                TempData["ErrorMessage"] = response.ErrorText;
                return RedirectToAction("Login", "Account");
            }

            TempData["Token"] = response.Token;
            return RedirectToAction("Login", "Account");
        }

    }
}
