using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
