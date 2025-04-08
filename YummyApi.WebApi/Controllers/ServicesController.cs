using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApiContext _context;

        public ServicesController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult ServiceList()
        {
            var values = _context.Services.ToList(); //tüm hizmetleri listele
            return Ok(values); //200 kodu döner ve Tüm Servisleri Listeler
        }

        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Servis Ekleme İşlemi Başarılı.."); //200 kodu döner hizmet ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteService(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound("Servis Bulunamadı.."); //404 kodu döner hizmet bulunamadı
            }
            _context.Services.Remove(service);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Servis Silme İşlemi Başarılı.."); //200 kodu döner hizmet silme işlemi başarılı
        }
        [HttpGet("{id}")]
        public IActionResult GetService(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound("Servis Bulunamadı.."); //404 kodu döner hizmet bulunamadı
            }
            return Ok(service); //200 kodu döner ve hizmet bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateService(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Servis Güncelleme İşlemi Başarılı.."); //200 kodu döner hizmet güncelleme işlemi başarılı
        }
    }
}
