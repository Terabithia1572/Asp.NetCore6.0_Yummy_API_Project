using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using YummyApi.WebUI.DTOs.CategoryDTOs;
using YummyApi.WebUI.DTOs.ProductDTOs;

namespace YummyApi.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProductList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Products");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Categories"); // Kategorileri almak için API'ye GET isteği gönderir.
            if (responseMessage.IsSuccessStatusCode) // Eğer yanıt başarılıysa (HTTP durum kodu 2xx ise)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Kategorileri JSON verisinden okur.
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData); // Kategorileri JSON verisinden listeye dönüştürür.
                List<SelectListItem> categories = values.Select(x => new SelectListItem // Kategorileri SelectListItem listesine dönüştürür.
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                }).ToList(); // Kategorileri SelectListItem listesine dönüştürür.
                ViewBag.Categories = categories; // Dönüştürülen kategorileri ViewBag'e atar, böylece görünümde kullanılabilir hale gelir.
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            var client = _httpClientFactory.CreateClient(); // Bu satır, denetleyiciye enjekte edilen fabrikayı kullanarak bir HTTP istemci örneği oluşturur.
            var jsonData = JsonConvert.SerializeObject(createProductDTO); // JSON verisine dönüştürme işlemi yapar.
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisini UTF-8 kodlamasıyla ve "application/json" içeriğiyle bir StringContent nesnesine sarar.
            var responseMessage = await client.PostAsync("https://localhost:44368/api/Products/CreateProductWithCategory", stringContent); // HTTP POST isteği gönderir ve yanıtı alır.
            if (responseMessage.IsSuccessStatusCode) // Eğer yanıt başarılıysa (HTTP durum kodu 2xx ise)
            {
                return RedirectToAction("ProductList"); // Kategori listesini görüntülemek için ProductList eylemine yönlendirir.
            }
            return View(); // Eğer yanıt başarısızsa, aynı görünümü tekrar gösterir.
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:44368/api/Products?id=" + id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44368/api/Products/GetProduct?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetProductByIDDTO>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44368/api/Products/", stringContent);
            return RedirectToAction("ProductList");
        }
    }
}
