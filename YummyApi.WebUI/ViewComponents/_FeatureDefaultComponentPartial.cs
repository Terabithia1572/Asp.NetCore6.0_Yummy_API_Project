using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.ViewComponents
{
    public class _FeatureDefaultComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
