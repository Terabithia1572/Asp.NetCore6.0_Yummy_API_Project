using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.ViewComponents.HomePageViewComponents
{
    public class _HomePageStatisticComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomePageStatisticComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            //Toplam Ürün Sayısı
            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:44368/api/Statistics/ProductCount");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            ViewBag.productCount = jsonData1;

            //Toplam Rezervasyon Sayısı
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("https://localhost:44368/api/Statistics/ReservationCount");
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.reservationCount = jsonData2;
            //Toplam Şef Sayısı
            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client3.GetAsync("https://localhost:44368/api/Statistics/ChefCount");
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.chefCount = jsonData3;
            //Toplam Müşteri Sayısı
            var client4 = _httpClientFactory.CreateClient();
            var responseMessage4 = await client4.GetAsync("https://localhost:44368/api/Statistics/CustomerCount");
            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
            ViewBag.customerCount = jsonData4;


            return View();
        }
    }
}
