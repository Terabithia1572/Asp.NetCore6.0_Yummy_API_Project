using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.ViewComponents
{
    public class _AboutDefaultComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
