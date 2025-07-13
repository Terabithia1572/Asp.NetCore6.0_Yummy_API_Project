using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YummyEventsController : ControllerBase
    {
        private readonly ApiContext _context;

        public YummyEventsController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult YummyEventList()
        {
            var values = _context.YummyEvents.ToList(); //tüm Etkinlikleri listele
            return Ok(values); //200 kodu döner ve Tüm Etkinlikleri Listeler
        }

        [HttpPost]
        public IActionResult CreateYummyEvent(YummyEvent YummyEvent)
        {
            _context.YummyEvents.Add(YummyEvent);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Etkinlik Ekleme İşlemi Başarılı.."); //200 kodu döner Etkinlik ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteYummyEvent(int id)
        {
            var YummyEvent = _context.YummyEvents.Find(id);
            if (YummyEvent == null)
            {
                return NotFound("Etkinlik Bulunamadı.."); //404 kodu döner Etkinlik bulunamadı
            }
            _context.YummyEvents.Remove(YummyEvent);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Etkinlik Silme İşlemi Başarılı.."); //200 kodu döner Etkinlik silme işlemi başarılı
        }
        [HttpGet("{id}")]
        public IActionResult GetYummyEvent(int id)
        {
            var YummyEvent = _context.YummyEvents.Find(id);
            if (YummyEvent == null)
            {
                return NotFound("Etkinlik Bulunamadı.."); //404 kodu döner Etkinlik bulunamadı
            }
            return Ok(YummyEvent); //200 kodu döner ve Etkinlik bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateYummyEvent(YummyEvent YummyEvent)
        {
            _context.YummyEvents.Update(YummyEvent);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Etkinlik Güncelleme İşlemi Başarılı.."); //200 kodu döner Etkinlik güncelleme işlemi başarılı
        }
    }
}
