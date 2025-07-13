using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyApi.WebUI.DTOs.EventDTOs;

namespace YummyApi.WebUI.ViewComponents
{
    public class _EventDefaultComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _EventDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/YummyEvents");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEventDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
