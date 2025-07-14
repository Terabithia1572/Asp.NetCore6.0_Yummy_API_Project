using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.ViewComponents.AdminLayoutNavbarViewComponents
{
    public class _NavbarFormInlineAdminLayoutViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
