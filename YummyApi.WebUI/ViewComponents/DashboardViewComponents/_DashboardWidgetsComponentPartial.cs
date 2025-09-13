using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyApi.WebUI.DTOs.CategoryDTOs;

namespace YummyApi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardWidgetsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardWidgetsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            int r1,r2, r3, r4;
            Random rnd = new Random();
            r1 = rnd.Next(1, 35);
            r2 = rnd.Next(1, 35);
            r3 = rnd.Next(1, 35);
            r4 = rnd.Next(1, 35);
            //Burada api den verileri çekip viewbag ile view e gönderiyoruz
            //Toplam Rezervasyon Sayısı
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Rezervations/GetTotalReservationCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.totalReservationSount = jsonData;
            ViewBag.r1 = r1;
            // var values = JsonConvert.DeserializeObject<int>(jsonData);
            //Toplam Müşteri Sayısı            
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("https://localhost:44368/api/Rezervations/GetTotalCustomerCount");
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.totalCustomerCount = jsonData2;
            ViewBag.r2 = r2;
            //Bekleyen Rezervasyon Sayısı
            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client3.GetAsync("https://localhost:44368/api/Rezervations/GetPendingReservation");
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.pendingReservation = jsonData3;
            ViewBag.r3 = r3;
            //Onaylanan Rezervasyon Sayısı
            var client4 = _httpClientFactory.CreateClient();
            var responseMessage4 = await client4.GetAsync("https://localhost:44368/api/Rezervations/GetApprovedReservation");
            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
            ViewBag.approvedReservation = jsonData4;
            ViewBag.r4 = r4;

            return View();
        }
    }
}
//https://localhost:44368/api/Rezervations/GetTotalReservationCount
//https://localhost:44368/api/Rezervations/GetTotalCustomerCount
//https://localhost:44368/api/Rezervations/GetPendingReservation
//https://localhost:44368/api/Rezervations/GetApprovedReservation
