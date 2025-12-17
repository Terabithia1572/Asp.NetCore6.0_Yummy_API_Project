using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardEmployeeTaskComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory; // HttpClientFactory için  Dependency injection geçtik

        public _DashboardEmployeeTaskComponentPartial(IHttpClientFactory httpClientFactory) // Constructor injection 
        {
            _httpClientFactory = httpClientFactory; // HttpClientFactory'yi alıyoruz
        }

        public async Task< IViewComponentResult> InvokeAsync() // Invoke metodu ViewComponent'in çalıştığı yerdir
        {

            return View(); // ViewComponent'in view'ini döndürüyoruz
        }
    }
}
