using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Painel.Helpers;
using Painel.Models;
using System.Threading.Tasks;

namespace Painel.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpContext _httpContext;
        private readonly UserManager _userManager;

        public UserController(
            IHttpContextAccessor httpContextAccessor,
            UserManager userManager)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserViewModel request)
        {
            bool IsUserAuthenticated = await _userManager.SignInAsync(_httpContext, request, true);

            if (IsUserAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "Dados Invalidos";

                return View("Login");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userManager.SignOutAsync(_httpContext);

            return View("Login");
        }
    }
}