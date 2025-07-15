using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }
    }
}
