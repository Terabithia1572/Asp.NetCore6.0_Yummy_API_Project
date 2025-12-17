using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly ApiContext _context;

        public EmployeeTasksController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult EmployeeTaskList()
        {
            var values = _context.EmployeeTasks.ToList(); //tüm hizmetleri listele
            return Ok(values); //200 kodu döner ve Tüm Görevleri Listeler
        }

        [HttpPost]
        public IActionResult CreateEmployeeTask(EmployeeTask EmployeeTask)
        {
            _context.EmployeeTasks.Add(EmployeeTask);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Görev Ekleme İşlemi Başarılı.."); //200 kodu döner hizmet ekleme işlemi başarılı
        }
        [HttpDelete]
        public IActionResult DeleteEmployeeTask(int id)
        {
            var EmployeeTask = _context.EmployeeTasks.Find(id);
            if (EmployeeTask == null)
            {
                return NotFound("Görev Bulunamadı.."); //404 kodu döner hizmet bulunamadı
            }
            _context.EmployeeTasks.Remove(EmployeeTask);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Görev Silme İşlemi Başarılı.."); //200 kodu döner hizmet silme işlemi başarılı
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployeeTask(int id)
        {
            var EmployeeTask = _context.EmployeeTasks.Find(id);
            if (EmployeeTask == null)
            {
                return NotFound("Görev Bulunamadı.."); //404 kodu döner hizmet bulunamadı
            }
            return Ok(EmployeeTask); //200 kodu döner ve hizmet bilgilerini getirir
        }
        [HttpPut]
        public IActionResult UpdateEmployeeTask(EmployeeTask EmployeeTask)
        {
            _context.EmployeeTasks.Update(EmployeeTask);
            _context.SaveChanges(); //değişiklikleri kaydet
            return Ok("Görev Güncelleme İşlemi Başarılı.."); //200 kodu döner hizmet güncelleme işlemi başarılı
        }
    }
}
