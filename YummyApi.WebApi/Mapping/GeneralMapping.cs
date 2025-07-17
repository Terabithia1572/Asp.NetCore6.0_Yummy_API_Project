using AutoMapper;
using YummyApi.WebApi.DTOs.AboutDTOs;
using YummyApi.WebApi.DTOs.CategoryDTOs;
using YummyApi.WebApi.DTOs.FeatureDTOs;
using YummyApi.WebApi.DTOs.MessageDTO;
using YummyApi.WebApi.DTOs.NotificationDTOs;
using YummyApi.WebApi.DTOs.ProductDTOs;
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
            CreateMap<Feature, CreateFeatureDTO>().ReverseMap(); // Öne Çıkan Oluşturma DTO'su
            CreateMap<Feature, UpdateFeatureDTO>().ReverseMap(); // Öne Çıkan Güncelleme DTO'su
            CreateMap<Feature, GetByIDFeatureDTO>().ReverseMap(); // ID ile Öne Çıkan Getirme DTO'su

            CreateMap<Message, ResultMessageDTO>().ReverseMap(); //Mesaj DTO'su
            CreateMap<Message, CreateMessageDTO>().ReverseMap(); //Mesaj Oluşturma DTO'su
            CreateMap<Message, UpdateMessageDTO>().ReverseMap(); //Mesaj Güncelleme DTO'su
            CreateMap<Message, GetByIDMessageDTO>().ReverseMap(); //ID ile Mesaj Getirme DTO'su


            CreateMap<Product, CreateProductDTO>().ReverseMap(); //Ürün Oluşturma DTO'su
            CreateMap<Product, ResultProductWithCategoryDTO>().ForMember(x=>x.CategoryName,y=>y.MapFrom(z=>z.Category.CategoryName)).ReverseMap(); //Ürün Kategorisi ile Birlikte Getirme DTO'su

            //ForMember burada önemli burada 2 parametre geçtik
            //1.parametre getirmek istediğim yani hangi propertyleri getirmek istedim CategoryName getirmek istedim
            //2.parametre Bu Kategori adını nereden getireceğim Category tablosundan Kategori Adını Getir Dedik

            CreateMap<Notification, ResultNotificationDTO>().ReverseMap(); //Bildirim DTO'su
            CreateMap<Notification, CreateNotificationDTO>().ReverseMap(); //Bildirim Oluşturma DTO'su
            CreateMap<Notification, GetNotificationByIDDTO>().ReverseMap(); //ID ile Bildirim Getirme DTO'su
            CreateMap<Notification, UpdateNotificationDTO>().ReverseMap(); //Bildirim Güncelleme DTO'su


            CreateMap<Category, CreateCategoryDTO>().ReverseMap(); //Kategori Oluşturma DTO'su
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap(); //Kategori Güncelleme DTO'su


            CreateMap<About, ResultAboutDTO>().ReverseMap(); //Hakkında DTO'su
            CreateMap<About, CreateAboutDTO>().ReverseMap(); //Hakkında Oluşturma DTO'su
            CreateMap<About, UpdateAboutDTO>().ReverseMap(); //Hakkında Güncelleme DTO'su
            CreateMap<About, GetAboutByIDDTO>().ReverseMap(); //ID ile Hakkında Getirme DTO'su
        }
    }
}
