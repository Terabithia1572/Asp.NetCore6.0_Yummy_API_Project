using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using YummyApi.WebUI.DTOs.ContactDTOs;

namespace YummyApi.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ContactList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Contacts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateContact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDTO createContactDTO)
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            var jsonData = JsonConvert.SerializeObject(createContactDTO); // JSON verisine dönüştürme işlemi yapar.
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisini UTF-8 kodlamasıyla ve "application/json" içeriğiyle bir StringContent nesnesine sarar.
            var responseMessage = await client.PostAsync("https://localhost:44368/api/Contacts", stringContent); // HTTP POST isteği gönderir ve yanıtı alır.
            if (responseMessage.IsSuccessStatusCode) // Eğer yanıt başarılıysa (HTTP durum kodu 2xx ise)
            {
                return RedirectToAction("ContactList"); // Kategori listesini görüntülemek için ContactList eylemine yönlendirir.
            }
            return View(); // Eğer yanıt başarısızsa, aynı görünümü tekrar gösterir.
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44368/api/Contacts?id=" + id);
            return RedirectToAction("ContactList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Contacts/GetContact?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetContactByIDDTO>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDTO updateContactDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateContactDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44368/api/Contacts/", stringContent);
            return RedirectToAction("ContactList");
        }
    }
}
