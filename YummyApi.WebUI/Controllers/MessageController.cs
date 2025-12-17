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
        private readonly ILogger<MessageController> _logger;

        // Yerel API endpoint'inizi burada merkezi tanımlayın
        private const string MessagesApiBase = "https://localhost:7020/api/Messages";

        public MessageController(IHttpClientFactory httpClientFactory,
                                 IConfiguration configuration,
                                 ILogger<MessageController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
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

            var client1 = new HttpClient(); // HttpClientFactory kullanmadan doğrudan HttpClient örneği oluşturuluyor.
            var apiKey = _configuration["HuggingFace:ApiKey"] ?? Environment.GetEnvironmentVariable("HUGGINGFACE_API_KEY"); client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey); // Hugging Face API anahtarı başlık olarak ekleniyor.
            try
            {
                var translateRequestBody = new // Çeviri isteği için istek gövdesi oluşturuluyor.
                {
                    inputs = createMessageDto.MessageDetail // Çevrilecek metin
                };
                var translateJson = System.Text.Json.JsonSerializer.Serialize(translateRequestBody); // İstek gövdesi JSON formatına dönüştürülüyor.
                var translateContent = new StringContent(translateJson, Encoding.UTF8, "application/json"); // JSON içeriği StringContent olarak hazırlanıyor.

                var translateResponse = await client1.PostAsync("https://api-inference.huggingface.co/models/Helsinki-NLP/opus-mt-tr-en", translateContent); // Çeviri modeli için POST isteği gönderiliyor.
                var translateResponseString = await translateResponse.Content.ReadAsStringAsync(); // Yanıt içeriği okunuyor.

                string englishText = createMessageDto.MessageDetail;    // Varsayılan olarak orijinal metin kullanılıyor.
                if (translateResponseString.TrimStart().StartsWith("[")) // Yanıtın geçerli bir JSON dizisi olup olmadığı kontrol ediliyor.
                {
                    var translateDoc = JsonDocument.Parse(translateResponseString); // Yanıt JSON olarak ayrıştırılıyor.
                    englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString(); // Çevrilmiş metin alınıyor.
                    //ViewBag.v = englishText;
                }

                var toxicRequestBody = new // Toksisite kontrolü için istek gövdesi oluşturuluyor.
                {
                    inputs = englishText // Toksisite kontrolü yapılacak metin
                };

                var toxicJson = System.Text.Json.JsonSerializer.Serialize(toxicRequestBody); // İstek gövdesi JSON formatına dönüştürülüyor.
                var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json"); // JSON içeriği StringContent olarak hazırlanıyor.
                var toxicResponse = await client1.PostAsync("https://api-inference.huggingface.co/models/unitary/toxic-bert", toxicContent); // Toksisite modeli için POST isteği gönderiliyor.
                var toxicResponseString = await toxicResponse.Content.ReadAsStringAsync(); // Yanıt içeriği okunuyor. 

                if (toxicResponseString.TrimStart().StartsWith("["))  // Yanıtın geçerli bir JSON dizisi olup olmadığı kontrol ediliyor.
                {
                    var toxicDoc = JsonDocument.Parse(toxicResponseString); // Yanıt JSON olarak ayrıştırılıyor.
                    foreach (var item in toxicDoc.RootElement[0].EnumerateArray()) // Tüm toksisite etiketleri üzerinde dönülüyor.
                    {
                        string label = item.GetProperty("label").GetString(); // Etiket adı alınıyor.
                        double score = item.GetProperty("score").GetDouble(); // Etiket skoru alınıyor.

                        if (score > 0.5)
                        {
                            createMessageDto.MessageStatus = "Toksik Mesaj"; // Eğer skor 0.5'ten büyükse mesaj toksik olarak işaretleniyor.
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(createMessageDto.MessageStatus)) // Eğer mesaj durumu henüz belirlenmemişse
                {
                    createMessageDto.MessageStatus = "Mesaj Alındı"; // Mesaj durumu "Mesaj Alındı" olarak ayarlanıyor.
                }
            }
            catch (Exception ex) // Herhangi bir hata durumunda
            {
                createMessageDto.MessageStatus = "Onay Bekliyor";  // Hata durumunda mesaj durumu "Onay Bekliyor" olarak ayarlanıyor.
            }


            var client2 = _httpClientFactory.CreateClient(); // HttpClientFactory kullanılarak yeni bir HttpClient örneği oluşturuluyor.
            var jsonData = JsonConvert.SerializeObject(createMessageDto); // createMessageDto nesnesi JSON formatına dönüştürülüyor.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // JSON içeriği StringContent olarak hazırlanıyor.
            var responseMessage = await client2.PostAsync("https://localhost:44368/api/Messages", stringContent); // Mesaj oluşturma API'sine POST isteği gönderiliyor.
            if (responseMessage.IsSuccessStatusCode) // Eğer yanıt başarılıysa
            {
                return RedirectToAction("MessageList"); // Mesaj listesini görüntülemek için MessageList eylemine yönlendiriliyor.
            }
            return View(); // Eğer yanıt başarısızsa, aynı görünüm tekrar gösteriliyor.
        }
    }

}

