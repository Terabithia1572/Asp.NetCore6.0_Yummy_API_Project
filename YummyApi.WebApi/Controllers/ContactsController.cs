using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.ContactDTOs;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;
        Contact contact = new Contact();

        public ContactsController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult ContactList()
        {
            var values = _context.Contacts.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateContact(CreateContactDTO createContactDTO)
        {
            contact.ContactEmail = createContactDTO.ContactEmail;
            contact.ContactAddress = createContactDTO.ContactAddress;
            contact.ContactMapLocation = createContactDTO.ContactMapLocation;
            contact.ContactOpenHours = createContactDTO.ContactOpenHours;
            contact.ContactPhone = createContactDTO.ContactPhone;
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı...");
        }
        [HttpDelete]
        public IActionResult DeleteContact(int id)
        {
            var values = _context.Contacts.Find(id);
            if (values == null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı...");
        }
        [HttpGet("GetContact")]
        public IActionResult GetContact(int id)
        {
            var values = _context.Contacts.Find(id);
            if (values == null)
            {
                return NotFound();
            }
            return Ok(values);
        }
        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDTO updateContactDTO)
        {

            contact.ContactEmail = updateContactDTO.ContactEmail;
            contact.ContactAddress = updateContactDTO.ContactAddress;
            contact.ContactMapLocation = updateContactDTO.ContactMapLocation;
            contact.ContactOpenHours = updateContactDTO.ContactOpenHours;
            contact.ContactPhone = updateContactDTO.ContactPhone;
            contact.ContactID = updateContactDTO.ContactID;
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return Ok("Güncelleme İşlemi Başarılı...");
        }
    }
}
