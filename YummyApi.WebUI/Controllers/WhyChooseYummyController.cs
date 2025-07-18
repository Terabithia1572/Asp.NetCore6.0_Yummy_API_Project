using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using YummyApi.WebUI.DTOs.WhyChooseYummyDTOs;

namespace YummyApi.WebUI.Controllers
{
    public class WhyChooseYummyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WhyChooseYummyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> WhyChooseYummyList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Services");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWhyChooseYummyDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateWhyChooseYummy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateWhyChooseYummy(CreateWhyChooseYummyDTO createWhyChooseYummyDTO)
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            var jsonData = JsonConvert.SerializeObject(createWhyChooseYummyDTO); // JSON verisine dönüştürme işlemi yapar.
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisini UTF-8 kodlamasıyla ve "application/json" içeriğiyle bir StringContent nesnesine sarar.
            var responseMessage = await client.PostAsync("https://localhost:44368/api/Services", stringContent); // HTTP POST isteği gönderir ve yanıtı alır.
            if (responseMessage.IsSuccessStatusCode) // Eğer yanıt başarılıysa (HTTP durum kodu 2xx ise)
            {
                return RedirectToAction("WhyChooseYummyList"); // Kategori listesini görüntülemek için ServiceList eylemine yönlendirir.
            }
            return View(); // Eğer yanıt başarısızsa, aynı görünümü tekrar gösterir.
        }

        public async Task<IActionResult> DeleteWhyChooseYummy(int id)
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            await client.DeleteAsync("https://localhost:44368/api/Services?id=" + id); // Belirtilen ID'ye sahip servisi silmek için HTTP DELETE isteği gönderir.
            return RedirectToAction("WhyChooseYummyList"); // Kategori listesini görüntülemek için ServiceList eylemine yönlendirir.
        }

        [HttpGet]
        public async Task<IActionResult> UpdateWhyChooseYummy(int id)
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Services/" + id); // Belirtilen ID'ye sahip servisin bilgilerini almak için HTTP GET isteği gönderir.
            var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisini okur.
            var value = JsonConvert.DeserializeObject<GetWhyChooseYummyByIDDTO>(jsonData); // JSON verisini GetWhyChooseYummyByIDDTO nesnesine dönüştürür.
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateWhyChooseYummy(UpdateWhyChooseYummyDTO updateWhyChooseYummyDto)
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            var jsonData = JsonConvert.SerializeObject(updateWhyChooseYummyDto); // JSON verisine dönüştürme işlemi yapar.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // JSON verisini UTF-8 kodlamasıyla ve "application/json" içeriğiyle bir StringContent nesnesine sarar.
            await client.PutAsync("https://localhost:44368/api/Services/", stringContent); // HTTP PUT isteği gönderir ve güncellenmiş servisi kaydeder.
            return RedirectToAction("WhyChooseYummyList");
        }
    }
}
