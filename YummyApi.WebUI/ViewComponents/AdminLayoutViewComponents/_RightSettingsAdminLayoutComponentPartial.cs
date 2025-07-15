using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.ViewComponents.AdminLayoutViewComponents
{
    public class _RightSettingsAdminLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
