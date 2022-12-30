using BookStore.Data.Entities;
using BookStore.Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly UserRepository _userRepository;

        public AccountController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<User> LoginAsync(string username, string password)
        {
            User user = _userRepository.GetUserWithUsernameAndPassword(username, password);
            if (user == null)
            {
                RedirectToAction("Login");
            }

            else
            {
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.Name),
                };
                var identity = new ClaimsIdentity(claims, "custom");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
            }

            return user;
        }


        [HttpGet]
        public async void Logout()
        {
            await HttpContext.SignOutAsync();
        }


    }
}
