using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardGroupReservationComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
