using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator<Product> _validator;
        private readonly ApiContext _apiContext;

        public ProductsController(IValidator<Product> validator, ApiContext apiContext)
        {
            _validator = validator;
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _apiContext.Products.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var validatorResult = _validator.Validate(product);
            if (validatorResult.IsValid)
            {
                _apiContext.Products.Add(product);
                _apiContext.SaveChanges();
                return Ok("Ürün Eklendi...");
            }
            else
            {
                return BadRequest(validatorResult.Errors);
            }
        }
    }
}
