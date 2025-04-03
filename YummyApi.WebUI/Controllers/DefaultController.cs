using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
