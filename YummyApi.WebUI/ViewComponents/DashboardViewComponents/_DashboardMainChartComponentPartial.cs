using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using YummyApi.WebUI.DTOs.RezervationDTOs;
using YummyApi.WebUI.Models;

namespace YummyApi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardMainChartComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardMainChartComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44368"); // API adresin burası olacak

            var response = await client.GetAsync("api/Rezervations/GetReservationStats");
            var json = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<ReservationChartDTO>>(json);

            return View(data);
        }
    }
}
