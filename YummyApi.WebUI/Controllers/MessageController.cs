using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using YummyApi.WebUI.DTOs.MessageDTOs;

namespace YummyApi.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MessageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Messages");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDTO createMessageDTO)
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            var jsonData = JsonConvert.SerializeObject(createMessageDTO); // JSON verisine dönüştürme işlemi yapar.
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisini UTF-8 kodlamasıyla ve "application/json" içeriğiyle bir StringContent nesnesine sarar.
            var responseMessage = await client.PostAsync("https://localhost:44368/api/Messages", stringContent); // HTTP POST isteği gönderir ve yanıtı alır.
            if (responseMessage.IsSuccessStatusCode) // Eğer yanıt başarılıysa (HTTP durum kodu 2xx ise)
            {
                return RedirectToAction("MessageList"); // Mesaj listesini görüntülemek için MessageList eylemine yönlendirir.
            }
            return View(); // Eğer yanıt başarısızsa, aynı görünümü tekrar gösterir.
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44368/api/Messages?id=" + id);
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Messages/GetMessage?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetMessageByIDDTO>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDTO updateMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44368/api/Messages/", stringContent);
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public async Task<IActionResult> AnswerMessageWithOpenAI(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Messages/GetMessage?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetMessageByIDDTO>(jsonData);
            return View(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetMessageJson(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44368/api/Messages/GetMessage?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                // API tarafındaki hata kodunu koruyalım (404/500 vs.)
                var errorBody = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorBody);
            }

            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<GetMessageByIDDTO>(json);
            return Json(dto); // same-origin JSON
        }

    }
}
