using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using YummyApi.WebUI.DTOs.AISuggestionDTOs;

namespace YummyApi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardAIDailyMenuSuggestionComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public _DashboardAIDailyMenuSuggestionComponentPartial(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var apiKey = _configuration["Groq:ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
                return View(new List<MenuSuggestionDTO>());

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.groq.com/openai/v1/");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            string prompt =
                "4 farklı dünya mutfağından tamamen rastgele günlük menü oluştur.\n\n" +
                "KURALLAR:\n" +
                "- 4 farklı ülke mutfağı\n" +
                "- Türkçe olacak\n" +
                "- ISO Country Code zorunlu\n" +
                "- SADECE JSON DÖNDÜR\n\n" +
                "FORMAT:\n" +
                "[\n" +
                "  {\n" +
                "    \"Cuisine\": \"X Mutfağı\",\n" +
                "    \"CountryCode\": \"XX\",\n" +
                "    \"MenuTitle\": \"Günlük Menü\",\n" +
                "    \"Items\": [\n" +
                "      { \"Name\": \"Yemek\", \"Description\": \"Açıklama\", \"Price\": 100 }\n" +
                "    ]\n" +
                "  }\n" +
                "]";

            var body = new
            {
                model = "llama-3.1-8b-instant", // ✅ DOĞRU MODEL
                messages = new[]
                {
                    new { role = "system", content = "Return ONLY valid JSON. No markdown." },
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return View(new List<MenuSuggestionDTO>());

            dynamic obj = JsonConvert.DeserializeObject(responseJson);
            string aiContent = obj.choices[0].message.content.ToString();

            aiContent = aiContent
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            List<MenuSuggestionDTO> menus;

            try
            {
                menus = JsonConvert.DeserializeObject<List<MenuSuggestionDTO>>(aiContent);
            }
            catch
            {
                menus = new List<MenuSuggestionDTO>();
            }

            return View(menus);
        }
    }
}