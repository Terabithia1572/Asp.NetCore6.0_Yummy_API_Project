using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyApi.WebUI.DTOs.ProductDTOs;

namespace YummyApi.WebUI.ViewComponents.DefaultMenuViewComponents
{
    public class _DefaultMenuProductComponentViewPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _DefaultMenuProductComponentViewPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Products");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
