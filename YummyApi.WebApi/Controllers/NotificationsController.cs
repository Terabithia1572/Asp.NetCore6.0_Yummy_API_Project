using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.NotificationDTOs;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _apiContext;

        public NotificationsController(IMapper mapper, ApiContext apiContext)
        {
            _mapper = mapper;
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult NotificationList()
        {
            var values = _apiContext.Notifications.ToList();
            return Ok(_mapper.Map<List<ResultNotificationDTO>>(values));
        }

        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDTO createNotificationDTO)
        {
            var values = _mapper.Map<Notification>(createNotificationDTO);
            _apiContext.Notifications.Add(values);
            _apiContext.SaveChanges();
            return Ok("Özellik Ekleme İşlemi Başarılı..");
        }
        [HttpDelete]
        public IActionResult DeleteNotification(int id)
        {
            var values = _apiContext.Notifications.Find(id);
            if (values == null)
            {
                return NotFound("Özellik Bulunamadı..");
            }
            _apiContext.Notifications.Remove(values);
            _apiContext.SaveChanges();
            return Ok("Özellik Silme İşlemi Başarılı..");
        }
        [HttpGet("{GetNotification}")]
        public IActionResult GetNotification(int id)
        {
            var values = _apiContext.Notifications.Find(id);
            if (values == null)
            {
                return NotFound("Özellik Bulunamadı..");
            }
            return Ok(_mapper.Map<GetNotificationByIDDTO>(values));
        }

        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDTO updateNotificationDTO)
        {
            var values = _mapper.Map<Notification>(updateNotificationDTO);
            _apiContext.Notifications.Update(values);
            _apiContext.SaveChanges();
            return Ok("Özellik Güncelleme İşlemi Başarılı..");
        }
    }
}
