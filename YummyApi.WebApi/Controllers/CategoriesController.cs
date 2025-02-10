using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;

        public CategoriesController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CategoryList()
        {
            var values=_context.Categories.ToList(); //tüm kategorileri listele
            return Ok(values); //200 kodu döner ve Tüm Kategorileri Listeler
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Kategori Ekleme İşlemi Başarılı.."); //200 kodu döner kategori ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Kategori Bulunamadı.."); //404 kodu döner kategori bulunamadı
            }
            _context.Categories.Remove(category);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Kategori Silme İşlemi Başarılı.."); //200 kodu döner kategori silme işlemi başarılı
        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Kategori Bulunamadı.."); //404 kodu döner kategori bulunamadı
            }
            return Ok(category); //200 kodu döner ve kategori bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Kategori Güncelleme İşlemi Başarılı.."); //200 kodu döner kategori güncelleme işlemi başarılı
        }
    }
}
