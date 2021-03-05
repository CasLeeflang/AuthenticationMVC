using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationMVC.Models;
using Microsoft.AspNetCore.Mvc;
using DataAccessModel;
using DataAccess;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult userSignUp()
        {
            return View();
        }

        public IActionResult loginPage()
        {
            return View();
        }

        [HttpPost("CreateUser")]
        public ActionResult CreateUser(UserView newUser)
        {
            bool admin;

            if (newUser.Admin)
            {
                admin = true;
            }
            else
            {
                admin = false;
            }

            User userDTO = new User
            {
                firstName = newUser.firstName,
                lastName = newUser.lastName,
                Password = newUser.Password,
                Admin = admin

            };

            UserManager.CreateUser(userDTO);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult Login(LoginModel LogDetails)
        {
            if (UserManager.IsValid(LogDetails.firstName, LogDetails.Password))
            {
                Console.WriteLine("Valid credentials!");               

                try
                {
                    Authenticate(LogDetails);
                    Console.WriteLine("Authenticated!");
                    
                    return RedirectToAction("Index", "Home");

                }
                catch
                {
                    Console.WriteLine("Authentication Failed!");
                    return RedirectToAction("loginPage");
                }
            }
            else
            {
                Console.WriteLine("Invalid credentials!");
                return RedirectToAction("loginPage");
            }
        }

        // Function to authenticate the user
        public void Authenticate(LoginModel LogDetails)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Name, LogDetails.firstName)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

            return;
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
