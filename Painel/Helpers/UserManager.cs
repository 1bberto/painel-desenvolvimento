using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Painel.Entities;
using Painel.Interfaces.Services;
using Painel.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Painel.Helpers
{
    public class UserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> SignInAsync(HttpContext httpContext, UserViewModel userViewModel, bool isPersistent = false)
        {
            var user = await _userService.AuthenticateAsync(userViewModel.Login, userViewModel.Password);

            if (user is not null)
            {
                ClaimsIdentity identity = new(GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new(identity);

                await httpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties()
                    {
                        IsPersistent = isPersistent
                    });

                return true;
            }

            return false;
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        private static IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new()
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Name, user.Login)
            };

            return claims;
        }
    }
}