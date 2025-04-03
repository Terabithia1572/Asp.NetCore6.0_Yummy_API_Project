using FluentValidation;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.ValidationRules
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x=>x.ProductName).NotEmpty().WithMessage("Ürün Adı Boş Geçilemez...");
            RuleFor(x => x.ProductName).MinimumLength(3).WithMessage("Ürün Adı En Az 3 Karakter Olmalıdır...");
            RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("Ürün Adı En Fazla 50 Karakter Olmalıdır...");
            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün Açıklaması Boş Geçilemez...");
            RuleFor(x => x.ProductDescription).MinimumLength(10).WithMessage("Ürün Açıklaması En Az 10 Karakter Olmalıdır...");
            RuleFor(x => x.ProductDescription).MaximumLength(200).WithMessage("Ürün Açıklaması En Fazla 200 Karakter Olmalıdır...");
            RuleFor(x => x.ProductPrice).NotEmpty().WithMessage("Ürün Fiyatı Boş Geçilemez...");
            RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("Ürün Fiyatı 0'dan Büyük Olmalıdır...");
            RuleFor(x => x.ProductPrice).LessThan(10000).WithMessage("Ürün Fiyatı 10.000'den Küçük Olmalıdır...");

        }
    }
}
