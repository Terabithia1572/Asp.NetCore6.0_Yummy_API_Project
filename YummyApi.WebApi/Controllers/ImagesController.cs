using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.ImageDTOs;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ImagesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult ImageList()
        {
            var values = _context.Images.ToList(); //tüm Resimleri listele
            return Ok(values); //200 kodu döner ve Tüm Resimleri Listeler
        }

        [HttpPost]
        public IActionResult CreateImage(CreateImageDTO createImageDTO)
        {
            //_context.Images.Add(Image);
            //_context.SaveChanges(); //değişiklikleri kaydet
            var values = _mapper.Map<Image>(createImageDTO);
            _context.Images.Add(values);
            _context.SaveChanges();
            return Ok("Resim Ekleme İşlemi Başarılı.."); //200 kodu döner Resim ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteImage(int id)
        {
            var Image = _context.Images.Find(id);
            if (Image == null)
            {
                return NotFound("Resim Bulunamadı.."); //404 kodu döner Resim bulunamadı
            }
            _context.Images.Remove(Image);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Resim Silme İşlemi Başarılı.."); //200 kodu döner Resim silme işlemi başarılı
        }
        [HttpGet("{GetImage}")]
        public IActionResult GetImage(int id)
        {
            var Image = _context.Images.Find(id);
            if (Image == null)
            {
                return NotFound("Resim Bulunamadı.."); //404 kodu döner Resim bulunamadı
            }
            return Ok(Image); //200 kodu döner ve Resim bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateImage(UpdateImageDTO updateImageDTO)
        {
            //_context.Images.Update(Image);
            //_context.SaveChanges(); //değişiklikleri kaydet
            var values = _mapper.Map<Image>(updateImageDTO);
            _context.Images.Update(values);
            _context.SaveChanges();
            return Ok("Resim Güncelleme İşlemi Başarılı.."); //200 kodu döner Resim güncelleme işlemi başarılı
        }
    }
}
