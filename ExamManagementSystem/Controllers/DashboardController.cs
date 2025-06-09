using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagementSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult SuperAdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Clerk")]
        public IActionResult ClerkDashboard()
        {
            return View();
        }
    }
}
