using System.Security.Claims;
using EmployeeManangement.Application.Enums;
using EmployeeManangement.Application.Interfaces.Repositories;
using EmployeeManangement.Application.ViewModels.Account;
using EmployeeManangement.Application.ViewModels.Employee;
using EmployeeManangement.Application.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountController(IUserRepository userRepository, IEmployeeRepository employeeRepository)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserIdentityModel model)
        {
            // For validation
            if (!ModelState.IsValid)
                return View(model);

            // Check if user already exists
            var user = await _userRepository.IsUserExists(model.Email);
            if (user)
            {
                ModelState.AddModelError("", "User already exists with this email.");
                TempData["UserExist"] = "User already exists with this email.";
                return View(model);
            }

         
            var userModel = new UserAddToViewModel
            {
                UserName = model.Email,
                Password = model.Password,
                RoleId = (int)eRole.Employee,
            };

            var createdUser = await _userRepository.Create(userModel);
            if (createdUser != null && createdUser.UserId > 0)
            {
                var employeeModel = new EmployeeToCreateViewModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    userId = createdUser.UserId,
                    DepartmentId = 1,
                };
                await _employeeRepository.Create(employeeModel);
            }
            else
            {
                ModelState.AddModelError("", "User creation failed.");
                return View(model);
            }

            TempData["Message"] = "You have registered successfully.";
            return RedirectToAction("SignIn");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(LogInToViewModel model)
        {
            // For validation
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userRepository.Authenticate(model.Email, model.Password);
            if (user != null && user.UserId > 0)
            {
                await CreateAuthCookie(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["SignInError"] = "Your credentials do not match.";
                return View(model);
            }
        }

        private async Task CreateAuthCookie(UserToViewModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FullName", user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.RoleName),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // Custom authentication properties can be set here
                // AllowRefresh = true,
                // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // IsPersistent = true,
                // IssuedUtc = DateTimeOffset.UtcNow,
                // RedirectUri = "/Home/Index"
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
