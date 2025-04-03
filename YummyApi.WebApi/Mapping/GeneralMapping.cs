using AutoMapper;
using YummyApi.WebApi.DTOs.FeatureDTOs;
using YummyApi.WebApi.DTOs.MessageDTO;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Feature, ResultFeatureDTO>().ReverseMap();
            //        AutoMapper'deki ReverseMap() metodu, iki yönlü dönüşüm sağlar. Yani, bir nesneyi diğerine dönüştürdüğünüz gibi, tersini de otomatik olarak yapmanıza olanak tanır.
            //        Özetle:
            // Tek satırla çift yönlü mapleme sağlar.
            // Kod tekrarını azaltır.
            // Kolay kullanım sunar.
            CreateMap<Feature, CreateFeatureDTO>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDTO>().ReverseMap();
            CreateMap<Feature, GetByIDFeatureDTO>().ReverseMap();

            CreateMap<Message, ResultMessageDTO>().ReverseMap();
            CreateMap<Message, CreateMessageDTO>().ReverseMap();
            CreateMap<Message, UpdateMessageDTO>().ReverseMap();
            CreateMap<Message, GetByIDMessageDTO>().ReverseMap();
        }
    }
}
