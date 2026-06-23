using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
