using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.DTOs.ProductDTOs
{
    public class ResultProductWithCategoryDTO
    {

        public int ProductID { get; set; } //Ürün ID
        public string ProductName { get; set; } //Ürün Adı
        public string ProductDescription { get; set; } //Ürün Açıklaması
        public decimal ProductPrice { get; set; } // Ürün Fiyatı
        public string ProductImageURL { get; set; } //Ürün Resmi URL

        public int CategoryID { get; set; } //? Boş geçilebilir anlamı katıyor.. Kategori ile Ürünleri İlişkili hale getirdik
        public string CategoryName { get; set; } 
    }
}
