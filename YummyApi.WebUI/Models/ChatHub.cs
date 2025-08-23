using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Text;

namespace YummyApi.WebUI.Models
{
    public class ChatHub : Hub
    {
        private const string apiKey = ""; //Buraya OpenAI API anahtarınızı ekleyeceğiz
        private const string modelGpt = "gpt-3.5-turbo"; //Kullanmak istediğiniz modeli buraya ekledik
        private readonly IHttpClientFactory _httpClientFactory; // HttpClientFactory'yi ekledik

        public ChatHub(IHttpClientFactory httpClientFactory) // Constructor ile HttpClientFactory'yi aldık
        {
            _httpClientFactory = httpClientFactory; // HttpClientFactory'yi atadık
        }

        private static readonly Dictionary<string, List<Dictionary<string, string>>> _history = new(); // Kullanıcı geçmişini tutmak için bir sözlük
        public override Task OnConnectedAsync() // Bağlantı kurulduğunda çağrılır
        {
            _history[Context.ConnectionId] = new List<Dictionary<string, string>>{ // Her kullanıcı için başlangıç mesajı
    new Dictionary<string,string> // Sistem mesajı
    {
        ["role"] = "system", // Rolü sistem olarak ayarladık
        ["content"] = "You are a helpful assistant that helps people find information about YummyApi, a fictional food recipe API. You can provide details about its features, usage, and how to integrate it into applications." 
        // Sistem mesajı içeriği
    }};

            return base.OnConnectedAsync(); // Temel sınıfın OnConnectedAsync metodunu çağırdık
        }
        public override Task OnDisconnectedAsync(Exception? exception) // Bağlantı kesildiğinde çağrılır
        {
            _history.Remove(Context.ConnectionId); // Kullanıcı geçmişini siler
            return base.OnDisconnectedAsync(exception); // Temel sınıfın OnDisconnectedAsync metodunu çağırdık
        }
        public async Task SendMessage(string userMessage) // Kullanıcıdan mesaj alır
        {
            await Clients.Caller.SendAsync("ReceiveUserEcho", userMessage); // Kullanıcıya mesajını geri gönderir
            var history = _history[Context.ConnectionId]; // Kullanıcının geçmişini alır
            history.Add(new Dictionary<string, string> // Kullanıcı mesajını geçmişe ekler
            {
                ["role"] = "user", // Rolü kullanıcı olarak ayarladık
                ["content"] = userMessage,// Mesaj içeriği

            });
            await StreamOpenAI(history, Context.ConnectionAborted);
        }
        public async Task StreamOpenAI(List<Dictionary<string, string>> history, CancellationToken cancellationToken) // OpenAI'den yanıt alır
        {
            var client = _httpClientFactory.CreateClient("openai"); // HttpClient oluşturur
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey); // Yetkilendirme başlığı ekler
            var payload = new // İstek yükünü oluşturur
            {
                model = modelGpt, // Modeli ayarlar
                messages = history, // Mesaj geçmişini ekler
                stream = true, // Akış modunu etkinleştirir
                temperature = 0.2 // Yanıtın çeşitliliğini ayarlar

            };
            using var request = new HttpRequestMessage(HttpMethod.Post, "v1/chat/completions"); // İstek oluşturur
            request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"); // Yükü JSON'a dönüştürür

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken); // İsteği gönderir ve yanıtı alır
            response.EnsureSuccessStatusCode(); // Başarılı yanıt alındığından emin olur
            using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken); // Yanıt akışını alır
            using var reader = new StreamReader(responseStream); // Akışı okuyucuya sarar




        }
    }
}



