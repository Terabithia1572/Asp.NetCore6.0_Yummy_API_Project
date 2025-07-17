using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.AboutDTOs;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public AboutsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList(); //tüm Hakkındaleri listele
            return Ok(values); //200 kodu döner ve Tüm Hakkındaleri Listeler
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDTO createAboutDTO)
        {
            //_context.Abouts.Add(About);
            //_context.SaveChanges(); //değişiklikleri kaydet
            var values = _mapper.Map<About>(createAboutDTO);
            _context.Abouts.Add(values);
            _context.SaveChanges();
            return Ok("Hakkında Ekleme İşlemi Başarılı.."); //200 kodu döner Hakkında ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {
            var About = _context.Abouts.Find(id);
            if (About == null)
            {
                return NotFound("Hakkında Bulunamadı.."); //404 kodu döner Hakkında bulunamadı
            }
            _context.Abouts.Remove(About);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Hakkında Silme İşlemi Başarılı.."); //200 kodu döner Hakkında silme işlemi başarılı
        }
        [HttpGet("{GetAbout}")]
        public IActionResult GetAbout(int id)
        {
            var About = _context.Abouts.Find(id);
            if (About == null)
            {
                return NotFound("Hakkında Bulunamadı.."); //404 kodu döner Hakkında bulunamadı
            }
            return Ok(About); //200 kodu döner ve Hakkında bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            //_context.Abouts.Update(About);
            //_context.SaveChanges(); //değişiklikleri kaydet
            var values = _mapper.Map<About>(updateAboutDTO);
            _context.Abouts.Update(values);
            _context.SaveChanges();
            return Ok("Hakkında Güncelleme İşlemi Başarılı.."); //200 kodu döner Hakkında güncelleme işlemi başarılı
        }
    }
}
