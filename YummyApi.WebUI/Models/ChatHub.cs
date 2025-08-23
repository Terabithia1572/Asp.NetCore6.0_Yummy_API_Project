using Microsoft.AspNetCore.SignalR;

namespace YummyApi.WebUI.Models
{
    public class ChatHub : Hub
    {
        private const string apiKey = ""; //Buraya OpenAI API anahtarınızı ekleyeceğiz
        private const string model = "gpt-3.5-turbo"; //Kullanmak istediğiniz modeli buraya ekledik
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
        public async Task SendMessage(string message) // Kullanıcıdan mesaj alır
        {
           
        }
    }
}
