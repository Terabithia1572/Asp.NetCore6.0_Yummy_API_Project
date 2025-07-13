using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyApi.WebUI.DTOs.ChefDTOs;

namespace YummyApi.WebUI.ViewComponents
{
    public class _ChefDefaultComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _ChefDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Chefs");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultChefDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
