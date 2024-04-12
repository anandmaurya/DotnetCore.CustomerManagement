using Microsoft.AspNetCore.Mvc;

namespace CustomerAPP.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
    }
}
