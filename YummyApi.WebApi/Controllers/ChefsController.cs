using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ChefsController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult ChefList()
        {
            var values = _context.Chefs.ToList(); //tüm şefleri listele
            return Ok(values); //200 kodu döner ve Tüm Şefleri Listeler
        }
        [HttpPost]
        public IActionResult CreateChef(Chef chef)
        {
            _context.Chefs.Add(chef); //Chefs tablosuna chef ekle
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Şef Ekleme İşlemi Başarılı.."); //200 kodu döner şef ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteChef(int id)
        {
            var chef = _context.Chefs.Find(id); //id'ye göre şefi bul
            if (chef == null) //şef bulunamadıysa
            {
                return NotFound("Şef Bulunamadı.."); //404 kodu döner şef bulunamadı
            }
            _context.Chefs.Remove(chef);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Şef Silme İşlemi Başarılı.."); //200 kodu döner şef silme işlemi başarılı
        }
        [HttpGet("{id}")]
        public IActionResult GetChef(int id)
        {
            var chef = _context.Chefs.Find(id); //id'ye göre şefi bul
            if (chef == null) //şef bulunamadıysa
            {
                return NotFound("Şef Bulunamadı.."); //404 kodu döner şef bulunamadı
            }
            return Ok(chef); //200 kodu döner ve şef bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateChef(Chef chef)
        {
            _context.Chefs.Update(chef); //Chefs tablosundaki şefi güncelle
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Şef Güncelleme İşlemi Başarılı.."); //200 kodu döner şef güncelleme işlemi başarılı
        }
    }
}
