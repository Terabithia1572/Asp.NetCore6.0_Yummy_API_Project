using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace YummyApi.WebUI.Controllers
{
    public class AIController : Controller
    {
        public IActionResult CreateRecipe()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecipeWithOpenAI(string prompt)
        {
            var apiKey = ""; // OpenAI API anahtarınızı buraya girin
            using var client = new HttpClient(); // OpenAI API istemcisi
            client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer", apiKey); // OpenAI API anahtarınızı buraya girin
            var requestBody = new //
            {
                model = "gpt-3.5-turbo", // OpenAI modeli
                messages = new[] // Mesajlar, sistem ve kullanıcı girdisi
                {
                    new { role = "system", content = "Sen Bir Restoran için yemek önerileri yapan bir yapay zeka aracısın." +
                    "Amacımız kullanıcı tarafından girilen malzemelere göre yemek tarifisi önerisinde bulunamk." },
                    new { role = "user", content = prompt } // Kullanıcının girdiği malzeme bilgisi
                },
                temperature = 0.7 // 0.7, daha yaratıcı ve çeşitli cevaplar üretir
            };
            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody); // OpenAI API'ye istek gönder
            if(response.IsSuccessStatusCode)
            {
                var result=await response.Content.ReadFromJsonAsync<OpenAIResponse>(); // API'den gelen cevabı OpenAIResponse modeline deserialize et
                var content = result.choices[0].message.content; // İlk cevabın içeriğini al
                ViewBag.Recipe = content; // Cevabı ViewBag'e ata

            }
            else
            {
                ViewBag.Recipe = "Bir hata oluştu. Lütfen tekrar deneyin."; // Hata durumunda kullanıcıya mesaj göster
            }
            return View();
        }
        public class OpenAIResponse
        {
           public List<Choice> choices { get; set; }
        }
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
}
