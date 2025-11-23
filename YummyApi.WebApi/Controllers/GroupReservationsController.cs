using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.GroupReservationDTOs;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupReservationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GroupReservationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GroupReservationList()
        {
            var values = _context.GroupReservations.ToList(); //tüm Grup rezervasyonlarıleri listele
            return Ok(values); //200 kodu döner ve Tüm Grup rezervasyonlarıleri Listeler
        }

        [HttpPost]
        public IActionResult CreateGroupReservation(CreateGroupReservationDTO createGroupReservationDTO)
        {
            //_context.GroupReservations.Add(GroupReservation);
            //_context.SaveChanges(); //değişiklikleri kaydet
            var values = _mapper.Map<GroupReservation>(createGroupReservationDTO);
            _context.GroupReservations.Add(values);
            _context.SaveChanges();
            return Ok("Grup rezervasyonları Ekleme İşlemi Başarılı.."); //200 kodu döner Grup rezervasyonları ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteGroupReservation(int id)
        {
            var GroupReservation = _context.GroupReservations.Find(id);
            if (GroupReservation == null)
            {
                return NotFound("Grup rezervasyonları Bulunamadı.."); //404 kodu döner Grup rezervasyonları bulunamadı
            }
            _context.GroupReservations.Remove(GroupReservation);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Grup rezervasyonları Silme İşlemi Başarılı.."); //200 kodu döner Grup rezervasyonları silme işlemi başarılı
        }
        [HttpGet("{GetGroupReservation}")]
        public IActionResult GetGroupReservation(int id)
        {
            var GroupReservation = _context.GroupReservations.Find(id);
            if (GroupReservation == null)
            {
                return NotFound("Grup rezervasyonları Bulunamadı.."); //404 kodu döner Grup rezervasyonları bulunamadı
            }
            return Ok(GroupReservation); //200 kodu döner ve Grup rezervasyonları bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateGroupReservation(UpdateGroupReservationDTO updateGroupReservationDTO)
        {
            //_context.GroupReservations.Update(GroupReservation);
            //_context.SaveChanges(); //değişiklikleri kaydet
            var values = _mapper.Map<GroupReservation>(updateGroupReservationDTO);
            _context.GroupReservations.Update(values);
            _context.SaveChanges();
            return Ok("Grup rezervasyonları Güncelleme İşlemi Başarılı.."); //200 kodu döner Grup rezervasyonları güncelleme işlemi başarılı
        }
    }
}
