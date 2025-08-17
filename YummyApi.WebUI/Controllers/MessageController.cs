using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using YummyApi.WebUI.DTOs.MessageDTOs;
using static YummyApi.WebUI.Controllers.AIController;

namespace YummyApi.WebUI.Controllers
{
    // DeepSeek yanıt modeli (OpenAI ile neredeyse aynı şema)
    public class ChatResponse
    {
        public Choice[] choices { get; set; }
        public class Choice
        {
            public Message message { get; set; }
        }
        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }
    }

    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MessageController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
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
        public async Task<IActionResult> AnswerMessageWithOpenAI(int id, string prompt)
        {
            // 1) Mesajı kendi API'nden al
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44368/api/Messages/GetMessage?id={id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                ViewBag.answerAI = "Mesaj alınamadı.";
                return View(new GetMessageByIDDTO { MessageID = id });
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetMessageByIDDTO>(jsonData);

            // 2) Prompt hazırla
            prompt = value?.MessageDetail ?? string.Empty;

            // 3) OpenRouter API anahtarı (Secrets/ENV)
            var apiKey = _configuration["OpenRouter:ApiKey"]
                         ?? Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                ViewBag.answerAI = "OpenRouter API anahtarı bulunamadı.";
                return View(value);
            }

            // 4) OpenRouter'a istek gönder (DeepSeek R1: free)
            using var http = _httpClientFactory.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Önerilen ek başlıklar:
            http.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost"); // kendi siten/adresin
            http.DefaultRequestHeaders.Add("X-Title", "Yummy Admin");           // uygulama adı

            var requestBody = new
            {
                model = "deepseek/deepseek-r1:free",
                messages = new[]
                {
            new { role = "system", content =
                "Sen bir restoran için müşterilere nazik, profesyonel ve çözüm odaklı e-posta yanıtları yazan bir asistansın. " +
                "Müşteri memnuniyetini önceliklendir; teşekkür et, gerekirse özür dile, net çözüm önerileri ver; kısa ve anlaşılır paragraflar kullan."},
            new { role = "user", content = prompt }
        },
                temperature = 0.7
            };

            var resp = await http.PostAsJsonAsync("https://openrouter.ai/api/v1/chat/completions", requestBody);

            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadFromJsonAsync<ChatResponse>();
                var content = result?.choices?.FirstOrDefault()?.message?.content?.Trim();
                ViewBag.answerAI = string.IsNullOrWhiteSpace(content) ? "Yanıt boş döndü." : content;
            }
            else
            {
                var err = await resp.Content.ReadAsStringAsync();
                ViewBag.answerAI = $"OpenRouter hatası: {(int)resp.StatusCode} {resp.ReasonPhrase}\n{err}";
            }

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
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDTO createMessageDto)
        {

            var client1 = new HttpClient();
            var apiKey = _configuration["HuggingFace:ApiKey"] ?? Environment.GetEnvironmentVariable("HUGGINGFACE_API_KEY"); client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            try
            {
                var translateRequestBody = new
                {
                    inputs = createMessageDto.MessageDetail
                };
                var translateJson = System.Text.Json.JsonSerializer.Serialize(translateRequestBody);
                var translateContent = new StringContent(translateJson, Encoding.UTF8, "application/json");

                var translateResponse = await client1.PostAsync("https://api-inference.huggingface.co/models/Helsinki-NLP/opus-mt-tr-en", translateContent);
                var translateResponseString = await translateResponse.Content.ReadAsStringAsync();

                string englishText = createMessageDto.MessageDetail;
                if (translateResponseString.TrimStart().StartsWith("["))
                {
                    var translateDoc = JsonDocument.Parse(translateResponseString);
                    englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString();
                    //ViewBag.v = englishText;
                }

                var toxicRequestBody = new
                {
                    inputs = englishText
                };

                var toxicJson = System.Text.Json.JsonSerializer.Serialize(toxicRequestBody);
                var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json");
                var toxicResponse = await client1.PostAsync("https://api-inference.huggingface.co/models/unitary/toxic-bert", toxicContent);
                var toxicResponseString = await toxicResponse.Content.ReadAsStringAsync();

                if (toxicResponseString.TrimStart().StartsWith("["))
                {
                    var toxicDoc = JsonDocument.Parse(toxicResponseString);
                    foreach (var item in toxicDoc.RootElement[0].EnumerateArray())
                    {
                        string label = item.GetProperty("label").GetString();
                        double score = item.GetProperty("score").GetDouble();

                        if (score > 0.5)
                        {
                            createMessageDto.MessageStatus = "Toksik Mesaj";
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(createMessageDto.MessageStatus))
                {
                    createMessageDto.MessageStatus = "Mesaj Alındı";
                }
            }
            catch (Exception ex)
            {
                createMessageDto.MessageStatus = "Onay Bekliyor";
            }


            var client2 = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client2.PostAsync("https://localhost:7020/api/Messages", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }
    }

}

