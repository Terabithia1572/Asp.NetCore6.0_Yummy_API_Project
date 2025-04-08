using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyApi.WebUI.DTOs.ServiceDTOs;

namespace YummyApi.WebUI.ViewComponents
{
    public class _ServiceDefaultComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ServiceDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            var client=_httpClientFactory.CreateClient();
            var responseMessage= await client.GetAsync("https://localhost:44368/api/Services");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
