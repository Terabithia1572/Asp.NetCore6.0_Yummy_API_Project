using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ApiContext _context;

        public StatisticsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("productcount")]
        public IActionResult ProductCount()
        {
            var productCount = _context.Products.Count();
            return Ok(productCount);
        }

        [HttpGet("reservationcount")]
        public IActionResult ReservationCount()
        {
            var reservationCount = _context.Reservations.Count();
            return Ok(reservationCount);
        }

        [HttpGet("chefcount")]
        public IActionResult ChefCount()
        {
            var chefCount = _context.Chefs.Count();
            return Ok(chefCount);
        }
        [HttpGet("customercount")]
        public IActionResult CustomerCount()
        {
            var customerCount = _context.Reservations.Sum(x=>x.CountOfPeople);
            return Ok(customerCount);
        }
    }
}
