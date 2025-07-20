using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using YummyApi.WebUI.DTOs.ImageDTOs;

namespace YummyApi.WebUI.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ImageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Images");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultImageDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateImage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateImage(CreateImageDTO createImageDTO)
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            var jsonData = JsonConvert.SerializeObject(createImageDTO); // JSON verisine dönüştürme işlemi yapar.
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisini UTF-8 kodlamasıyla ve "application/json" içeriğiyle bir StringContent nesnesine sarar.
            var responseMessage = await client.PostAsync("https://localhost:44368/api/Images", stringContent); // HTTP POST isteği gönderir ve yanıtı alır.
            if (responseMessage.IsSuccessStatusCode) // Eğer yanıt başarılıysa (HTTP durum kodu 2xx ise)
            {
                return RedirectToAction("ImageList"); // Resim listesini görüntülemek için ImageList eylemine yönlendirir.
            }
            return View(); // Eğer yanıt başarısızsa, aynı görünümü tekrar gösterir.
        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44368/api/Images?id=" + id);
            return RedirectToAction("ImageList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateImage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Images/GetImage?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetImageByIDDTO>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateImage(UpdateImageDTO updateImageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateImageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44368/api/Images/", stringContent);
            return RedirectToAction("ImageList");
        }
    }
}
