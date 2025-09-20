using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.RezervationDTOs;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public RezervationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult RezervationList()
        {
            var values = _context.Reservations.ToList(); //tüm Rezervasyonleri listele
            return Ok(values); //200 kodu döner ve Tüm Rezervasyonleri Listeler
        }

        [HttpPost]
        public IActionResult CreateRezervation(CreateRezervationDTO createRezervationDTO)
        {
            //_context.Rezervations.Add(Rezervation);
            //_context.SaveChanges(); //değişiklikleri kaydet
            var values = _mapper.Map<Reservation>(createRezervationDTO);
            _context.Reservations.Add(values);
            _context.SaveChanges();
            return Ok("Rezervasyon Ekleme İşlemi Başarılı.."); //200 kodu döner Rezervasyon ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteRezervation(int id)
        {
            var Rezervation = _context.Reservations.Find(id);
            if (Rezervation == null)
            {
                return NotFound("Rezervasyon Bulunamadı.."); //404 kodu döner Rezervasyon bulunamadı
            }
            _context.Reservations.Remove(Rezervation);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Rezervasyon Silme İşlemi Başarılı.."); //200 kodu döner Rezervasyon silme işlemi başarılı
        }
        [HttpGet("{GetRezervation}")]
        public IActionResult GetRezervation(int id)
        {
            var Rezervation = _context.Reservations.Find(id);
            if (Rezervation == null)
            {
                return NotFound("Rezervasyon Bulunamadı.."); //404 kodu döner Rezervasyon bulunamadı
            }
            return Ok(Rezervation); //200 kodu döner ve Rezervasyon bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateRezervation(UpdateRezervationDTO updateRezervationDTO)
        {
            //_context.Rezervations.Update(Rezervation);
            //_context.SaveChanges(); //değişiklikleri kaydet
            var values = _mapper.Map<Reservation>(updateRezervationDTO);
            _context.Reservations.Update(values);
            _context.SaveChanges();
            return Ok("Rezervasyon Güncelleme İşlemi Başarılı.."); //200 kodu döner Rezervasyon güncelleme işlemi başarılı
        }
        [HttpGet("GetTotalReservationCount")]
        public IActionResult GetTotalReservationCount()
        {
            var totalReservations = _context.Reservations.Count();
            return Ok(totalReservations); //200 kodu döner ve toplam rezervasyon sayısını getirir
        }
        [HttpGet("GetTotalCustomerCount")]
        public IActionResult GetTotalCustomerCount()
        {
            var totalCustomer = _context.Reservations.Sum(x=>x.CountOfPeople);
            return Ok(totalCustomer); //200 kodu döner ve toplam rezervasyon sayısını getirir
        }
        [HttpGet("GetPendingReservation")]
        public IActionResult GetPendingReservation()
        {
            var totalCustomer = _context.Reservations.Where(x => x.ReservationStatus=="Onay Bekliyor").Count();
            return Ok(totalCustomer); //200 kodu döner ve toplam rezervasyon sayısını getirir
        }
        [HttpGet("GetApprovedReservation")]
        public IActionResult GetApprovedReservation()
        {
            var totalCustomer = _context.Reservations.Where(x => x.ReservationStatus == "Onaylandı").Count();
            return Ok(totalCustomer); //200 kodu döner ve toplam rezervasyon sayısını getirir
        }
        [HttpGet("GetReservationStats")]
        public IActionResult GetReservationStats()
        {
            DateTime today = DateTime.Today; // Bugünün tarihi
            DateTime fourMonthsAgo = today.AddMonths(-3); // 3 ay öncesinin tarihi

            // 1. SQL tarafında sadece gruplama ve veri çekme
            var rawData = _context.Reservations // DbSet'inden başla
                .Where(r => r.ReservationDate >= fourMonthsAgo) // Son 3 ayın verisi
                .GroupBy(r => new { r.ReservationDate.Year, r.ReservationDate.Month }) // Yıl ve aya göre grupla
                .Select(g => new // Grup içindeki verilere göre yeni bir anonim nesne oluştur
                {
                    g.Key.Year, // Yıl
                    g.Key.Month, // Ay
                    Approved = g.Count(x => x.ReservationStatus == "Onaylandı"), // Duruma göre say
                    Pending = g.Count(x => x.ReservationStatus == "Onay Bekliyor"), // Duruma göre say
                    Canceled = g.Count(x => x.ReservationStatus == "İptal Edildi") // Duruma göre say
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month) // Yıla ve aya göre sırala
                .ToList(); // Burala kadar SQL tarafında çalışır ve veriyi belleğe alır ve bir liste oluşturur

            // 2. Bellekte DTO'ya mapleme + tarih formatlama 
            var result = rawData.Select(x => new ReservationChartDTO // DTO'ya mapleme
            {
                Month = new DateTime(x.Year, x.Month, 1).ToString("MMM yyyy"), // Ay adını ve yılı al
                Approved = x.Approved, // Durum sayıları    
                Pending = x.Pending, // Durum sayıları 
                Canceled = x.Canceled // Durum sayıları
            }).ToList();

            return Ok(result);
        }

    }
}
