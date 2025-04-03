using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.DTOs.FeatureDTOs;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _apiContext;

        public FeaturesController(IMapper mapper, ApiContext apiContext)
        {
            _mapper = mapper;
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult FeatureList()
        {
            var values= _apiContext.Features.ToList();
            return Ok(_mapper.Map<List<ResultFeatureDTO>>(values));
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDTO createFeatureDTO)
        {
            var values=_mapper.Map<Feature>(createFeatureDTO);
            _apiContext.Features.Add(values);
            _apiContext.SaveChanges();
            return Ok("Özellik Ekleme İşlemi Başarılı..");
        }
        [HttpDelete]
        public IActionResult DeleteFeature(int id)
        {
            var values = _apiContext.Features.Find(id);
            if (values == null)
            {
                return NotFound("Özellik Bulunamadı..");
            }
            _apiContext.Features.Remove(values);
            _apiContext.SaveChanges();
            return Ok("Özellik Silme İşlemi Başarılı..");
        }
        [HttpGet("{GetFeature}")]
        public IActionResult GetFeature(int id)
        {
            var values = _apiContext.Features.Find(id);
            if (values == null)
            {
                return NotFound("Özellik Bulunamadı..");
            }
            return Ok(_mapper.Map<GetByIDFeatureDTO>(values));
        }

        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDTO updateFeatureDTO)
        {
            var values = _mapper.Map<Feature>(updateFeatureDTO);
            _apiContext.Features.Update(values);
            _apiContext.SaveChanges();
            return Ok("Özellik Güncelleme İşlemi Başarılı..");
        }
    }
}
