using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ApiContext _context;

        public TestimonialsController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult TestimonialList()
        {
            var values = _context.Testimonials.ToList(); //tüm hizmetleri listele
            return Ok(values); //200 kodu döner ve Tüm Referansleri Listeler
        }

        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial Testimonial)
        {
            _context.Testimonials.Add(Testimonial);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Referans Ekleme İşlemi Başarılı.."); //200 kodu döner hizmet ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteTestimonial(int id)
        {
            var Testimonial = _context.Testimonials.Find(id);
            if (Testimonial == null)
            {
                return NotFound("Referans Bulunamadı.."); //404 kodu döner hizmet bulunamadı
            }
            _context.Testimonials.Remove(Testimonial);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Referans Silme İşlemi Başarılı.."); //200 kodu döner hizmet silme işlemi başarılı
        }
        [HttpGet("{id}")]
        public IActionResult GetTestimonial(int id)
        {
            var Testimonial = _context.Testimonials.Find(id);
            if (Testimonial == null)
            {
                return NotFound("Referans Bulunamadı.."); //404 kodu döner hizmet bulunamadı
            }
            return Ok(Testimonial); //200 kodu döner ve hizmet bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateTestimonial(Testimonial Testimonial)
        {
            _context.Testimonials.Update(Testimonial);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Referans Güncelleme İşlemi Başarılı.."); //200 kodu döner hizmet güncelleme işlemi başarılı
        }
    }
}
