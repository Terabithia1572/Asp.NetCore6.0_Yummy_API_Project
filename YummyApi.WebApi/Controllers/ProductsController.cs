using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.ProductDTOs;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator<Product> _validator;
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public ProductsController(IValidator<Product> validator, ApiContext apiContext, IMapper mapper)
        {
            _validator = validator;
            _apiContext = apiContext;
            _mapper = mapper;
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
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _apiContext.Products.Find(id);
            if (product != null)
            {
                _apiContext.Products.Remove(product);
                _apiContext.SaveChanges();
                return Ok("Ürün Silindi...");
            }
            else
            {
                return NotFound("Ürün Bulunamadı...");
            }
        }
        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _apiContext.Products.Find(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound("Ürün Bulunamadı...");
            }
        }
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var validatorResult = _validator.Validate(product);
            if (validatorResult.IsValid)
            {
                _apiContext.Products.Update(product);
                _apiContext.SaveChanges();
                return Ok("Ürün Başarıyla Güncellendi..");
            }
            else
            {
                return BadRequest(validatorResult.Errors);
            }
        }
        [HttpPost("CreateProductWithCategory")]
        public IActionResult CreateProductWithCategory(CreateProductDTO createProductDTO)
        {
            var values=_mapper.Map<Product>(createProductDTO);
            _apiContext.Products.Add(values);
            _apiContext.SaveChanges();
            return Ok("Ürün Ekleme İşlemi Başarılı..");
        }
    }
}
