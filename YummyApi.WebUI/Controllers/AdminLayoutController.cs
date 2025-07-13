using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
