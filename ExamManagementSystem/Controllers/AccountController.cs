//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using ExamManagementSystem.Models;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using System.Linq;
//using System.Collections.Generic;

//namespace ExamManagementSystem.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly RoleManager<IdentityRole> _roleManager;

//        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
//        }

//        public IActionResult Register()
//        {
//            if (!User.IsInRole("SuperAdmin"))
//            {
//                return RedirectToAction("Index", "Home");
//            }
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (!User.IsInRole("SuperAdmin"))
//            {
//                return RedirectToAction("Index", "Home");
//            }

//            if (ModelState.IsValid)
//            {
//                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
//                var result = await _userManager.CreateAsync(user, model.Password);

//                if (result.Succeeded)
//                {
//                    if (await _roleManager.RoleExistsAsync(model.Role))
//                    {
//                        await _userManager.AddToRoleAsync(user, model.Role);
//                    }

//                    TempData["Success"] = "User created successfully!";
//                    return RedirectToAction("Register");
//                }

//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//            }
//            return View(model);
//        }

//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

//                if (result.Succeeded)
//                {
//                    var user = await _userManager.FindByEmailAsync(model.Email);
//                    var roles = await _userManager.GetRolesAsync(user);

//                    if (roles.Contains("SuperAdmin"))
//                        return RedirectToAction("SuperAdminDashboard", "Dashboard");
//                    else if (roles.Contains("Admin"))
//                        return RedirectToAction("AdminDashboard", "Dashboard");
//                    else if (roles.Contains("Clerk"))
//                        return RedirectToAction("ClerkDashboard", "Dashboard");
//                    else
//                        return RedirectToAction("Index", "Home");
//                }

//                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//            }
//            return View(model);
//        }

//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return RedirectToAction("Login", "Account");
//        }

//        [Authorize(Roles = "SuperAdmin")]
//        public async Task<IActionResult> UserList()
//        {
//            var users = _userManager.Users.ToList();
//            var userList = new List<UserViewModel>();

//            foreach (var user in users)
//            {
//                var roles = await _userManager.GetRolesAsync(user);
//                userList.Add(new UserViewModel
//                {
//                    Id = user.Id,
//                    Email = user.Email,
//                    Role = roles.FirstOrDefault()
//                });
//            }

//            return View(userList);
//        }

//        [Authorize(Roles = "SuperAdmin")]
//        public async Task<IActionResult> EditUser(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var user = await _userManager.FindByIdAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            var roles = await _userManager.GetRolesAsync(user);

//            var model = new EditUserViewModel
//            {
//                Id = user.Id,
//                Email = user.Email,
//                Role = roles.FirstOrDefault()
//            };

//            ViewBag.Roles = new List<string> { "Admin", "Clerk" };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "SuperAdmin")]
//        public async Task<IActionResult> EditUser(EditUserViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.FindByIdAsync(model.Id);
//                if (user == null)
//                {
//                    return NotFound();
//                }

//                user.Email = model.Email;
//                user.UserName = model.Email;

//                var currentRoles = await _userManager.GetRolesAsync(user);
//                await _userManager.RemoveFromRolesAsync(user, currentRoles);
//                await _userManager.AddToRoleAsync(user, model.Role);

//                var result = await _userManager.UpdateAsync(user);
//                if (result.Succeeded)
//                {
//                    if (!string.IsNullOrWhiteSpace(model.NewPassword))
//                    {
//                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//                        var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

//                        if (!passwordResult.Succeeded)
//                        {
//                            foreach (var error in passwordResult.Errors)
//                            {
//                                ModelState.AddModelError(string.Empty, error.Description);
//                            }
//                            ViewBag.Roles = new List<string> { "Admin", "Clerk" };
//                            return View(model);
//                        }
//                    }

//                    TempData["Success"] = "User updated successfully!";
//                    return RedirectToAction(nameof(UserList));
//                }

//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//            }

//            ViewBag.Roles = new List<string> { "Admin", "Clerk" };
//            return View(model);
//        }

//        [Authorize(Roles = "SuperAdmin")]
//        public async Task<IActionResult> DeleteUser(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user == null)
//                return NotFound();

//            return View("DeleteUser", user); // 🛠️ View Name fixed
//        }

//        [Authorize(Roles = "SuperAdmin")]
//        [HttpPost, ActionName("DeleteUser")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteUserConfirmed(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user != null)
//                await _userManager.DeleteAsync(user);

//            return RedirectToAction("UserList"); // 🛠️ Fixed Redirect
//        }
//    }
//}























using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ExamManagementSystem.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;

namespace ExamManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // -------------------------
        // Login
        // -------------------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("SuperAdmin"))
                    return RedirectToAction("SuperAdminDashboard", "Dashboard");
                else if (roles.Contains("Admin"))
                    return RedirectToAction("AdminDashboard", "Dashboard");
                else if (roles.Contains("Clerk"))
                    return RedirectToAction("ClerkDashboard", "Dashboard");
                else
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // -------------------------
        // Logout
        // -------------------------
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // -------------------------
        // Register (SuperAdmin Only)
        // -------------------------
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }

                TempData["Success"] = "User created successfully!";
                return RedirectToAction("Register");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // -------------------------
        // User List (SuperAdmin)
        // -------------------------
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.ToList();
            var userList = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = roles.FirstOrDefault()
                });
            }

            return View(userList);
        }

        // -------------------------
        // Edit User
        // -------------------------
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };

            ViewBag.Roles = new List<string> { "Admin", "Clerk" };
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new List<string> { "Admin", "Clerk" };
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.UserName = model.Email;

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, model.Role);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                ViewBag.Roles = new List<string> { "Admin", "Clerk" };
                return View(model);
            }

            // Optional: change password
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    ViewBag.Roles = new List<string> { "Admin", "Clerk" };
                    return View(model);
                }
            }

            TempData["Success"] = "User updated successfully!";
            return RedirectToAction("UserList");
        }

        // -------------------------
        // Delete User
        // -------------------------
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View("DeleteUser", user);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.DeleteAsync(user);

            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction("UserList");
        }
    }
}












//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using ExamManagementSystem.Models;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Identity.UI.Services;

//namespace ExamManagementSystem.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly IEmailSender _emailSender;

//        public AccountController(UserManager<IdentityUser> userManager,
//                                 SignInManager<IdentityUser> signInManager,
//                                 RoleManager<IdentityRole> roleManager,
//                                 IEmailSender emailSender)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
//            _emailSender = emailSender;
//        }

//        // -------------------------
//        // Login
//        // -------------------------
//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

//            if (result.Succeeded)
//            {
//                var user = await _userManager.FindByEmailAsync(model.Email);
//                var roles = await _userManager.GetRolesAsync(user);

//                if (roles.Contains("SuperAdmin"))
//                    return RedirectToAction("SuperAdminDashboard", "Dashboard");
//                else if (roles.Contains("Admin"))
//                    return RedirectToAction("AdminDashboard", "Dashboard");
//                else if (roles.Contains("Clerk"))
//                    return RedirectToAction("ClerkDashboard", "Dashboard");
//                else
//                    return RedirectToAction("Index", "Home");
//            }

//            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//            return View(model);
//        }

//        // -------------------------
//        // Logout
//        // -------------------------
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return RedirectToAction("Login", "Account");
//        }

//        // -------------------------
//        // Register (SuperAdmin Only)
//        // -------------------------
//        [Authorize(Roles = "SuperAdmin")]
//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        [Authorize(Roles = "SuperAdmin")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
//            var result = await _userManager.CreateAsync(user, model.Password);

//            if (result.Succeeded)
//            {
//                if (await _roleManager.RoleExistsAsync(model.Role))
//                {
//                    await _userManager.AddToRoleAsync(user, model.Role);
//                }

//                TempData["Success"] = "User created successfully!";
//                return RedirectToAction("Register");
//            }

//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError(string.Empty, error.Description);
//            }

//            return View(model);
//        }

//        // -------------------------
//        // Forgot Password View
//        // -------------------------
//        public IActionResult ForgotPassword()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.FindByEmailAsync(model.Email);
//                if (user == null)
//                {
//                    ModelState.AddModelError(string.Empty, "No account found with that email.");
//                    return View(model);
//                }

//                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//                var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

//                // Send email with the reset link
//                await _emailSender.SendEmailAsync(model.Email, "Password Reset Request", $"Click the link to reset your password: {resetLink}");

//                TempData["Message"] = "Check your email for a password reset link.";
//                return RedirectToAction("Login", "Account");
//            }

//            return View(model);
//        }

//        // -------------------------
//        // Reset Password View
//        // -------------------------
//        public IActionResult ResetPassword(string token, string email)
//        {
//            return View(new ResetPasswordViewModel { Token = token, Email = email });
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.FindByEmailAsync(model.Email);
//                if (user == null)
//                {
//                    ModelState.AddModelError(string.Empty, "No account found with that email.");
//                    return View(model);
//                }

//                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
//                if (result.Succeeded)
//                {
//                    TempData["Message"] = "Password reset successful. You can now log in with your new password.";
//                    return RedirectToAction("Login", "Account");
//                }

//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//            }

//            return View(model);
//        }

//        // -------------------------
//        // User List (SuperAdmin)
//        // -------------------------
//        [Authorize(Roles = "SuperAdmin")]
//        public async Task<IActionResult> UserList()
//        {
//            var users = _userManager.Users.ToList();
//            var userList = new List<UserViewModel>();

//            foreach (var user in users)
//            {
//                var roles = await _userManager.GetRolesAsync(user);
//                userList.Add(new UserViewModel
//                {
//                    Id = user.Id,
//                    Email = user.Email,
//                    Role = roles.FirstOrDefault()
//                });
//            }

//            return View(userList);
//        }

//        // -------------------------
//        // Edit User
//        // -------------------------
//        [Authorize(Roles = "SuperAdmin")]
//        [HttpGet]
//        public async Task<IActionResult> EditUser(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user == null) return NotFound();

//            var roles = await _userManager.GetRolesAsync(user);

//            var model = new EditUserViewModel
//            {
//                Id = user.Id,
//                Email = user.Email,
//                Role = roles.FirstOrDefault()
//            };

//            ViewBag.Roles = new List<string> { "Admin", "Clerk" };
//            return View(model);
//        }

//        [Authorize(Roles = "SuperAdmin")]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditUser(EditUserViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                ViewBag.Roles = new List<string> { "Admin", "Clerk" };
//                return View(model);
//            }

//            var user = await _userManager.FindByIdAsync(model.Id);
//            if (user == null) return NotFound();

//            user.Email = model.Email;
//            user.UserName = model.Email;

//            var currentRoles = await _userManager.GetRolesAsync(user);
//            await _userManager.RemoveFromRolesAsync(user, currentRoles);
//            await _userManager.AddToRoleAsync(user, model.Role);

//            var result = await _userManager.UpdateAsync(user);
//            if (!result.Succeeded)
//            {
//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//                ViewBag.Roles = new List<string> { "Admin", "Clerk" };
//                return View(model);
//            }

//            // Optional: change password
//            if (!string.IsNullOrWhiteSpace(model.NewPassword))
//            {
//                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//                var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

//                if (!passwordResult.Succeeded)
//                {
//                    foreach (var error in passwordResult.Errors)
//                    {
//                        ModelState.AddModelError(string.Empty, error.Description);
//                    }
//                    ViewBag.Roles = new List<string> { "Admin", "Clerk" };
//                    return View(model);
//                }
//            }

//            TempData["Success"] = "User updated successfully!";
//            return RedirectToAction("UserList");
//        }

//        // -------------------------
//        // Delete User
//        // -------------------------
//        [Authorize(Roles = "SuperAdmin")]
//        [HttpGet]
//        public async Task<IActionResult> DeleteUser(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user == null) return NotFound();

//            return View("DeleteUser", user);
//        }

//        [Authorize(Roles = "SuperAdmin")]
//        [HttpPost, ActionName("DeleteUser")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteUserConfirmed(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user != null)
//                await _userManager.DeleteAsync(user);

//            TempData["Success"] = "User deleted successfully!";
//            return RedirectToAction("UserList");
//        }
//    }
//}
