using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.ViewComponents.AdminLayoutViewComponents
{
    public class _SidebarAdminLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
