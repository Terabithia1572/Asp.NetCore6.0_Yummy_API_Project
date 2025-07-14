using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.MessageDTO;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _apiContext;

        public MessagesController(ApiContext apiContext, IMapper mapper)
        {
            _apiContext = apiContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _apiContext.Messages.ToList();
            return Ok(_mapper.Map<List<ResultMessageDTO>>(values));
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDTO createMessageDTO)
        {
            var values = _mapper.Map<Message>(createMessageDTO);
            _apiContext.Messages.Add(values);
            _apiContext.SaveChanges();
            return Ok("Mesaj Ekleme İşlemi Başarılı..");
        }
        [HttpDelete]
        public IActionResult DeleteMessage(int id)
        {
            var values = _apiContext.Messages.Find(id);
            if (values == null)
            {
                return NotFound("Mesaj Bulunamadı..");
            }
            _apiContext.Messages.Remove(values);
            _apiContext.SaveChanges();
            return Ok("Mesaj Silme İşlemi Başarılı..");
        }
        [HttpGet("{GetMessage}")]
        public IActionResult GetMessage(int id)
        {
            var values = _apiContext.Messages.Find(id);
            if (values == null)
            {
                return NotFound("Mesaj Bulunamadı..");
            }
            return Ok(_mapper.Map<GetByIDMessageDTO>(values));
        }
        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDTO updateMessageDTO)
        {
            var values = _mapper.Map<Message>(updateMessageDTO);
            _apiContext.Messages.Update(values);
            _apiContext.SaveChanges();
            return Ok("Mesaj Güncelleme İşlemi Başarılı..");
        }

        [HttpGet("MessageListByIsReadFalse")]
        public IActionResult MessageListByIsReadFalse()
        {
            var values = _apiContext.Messages.Where(x => x.MessageIsRead == false).ToList();
            return Ok(values);
        }
    }
}
