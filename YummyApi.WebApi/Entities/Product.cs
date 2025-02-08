namespace YummyApi.WebApi.Entities
{
    public class Product
    {
        public int ProductID { get; set; } //Ürün ID
        public string ProductName { get; set; } //Ürün Adı
        public string ProductDescription { get; set; } //Ürün Açıklaması
        public decimal ProductPrice { get; set; } // Ürün Fiyatı
        public string ProductImageURL { get; set; } //Ürün Resmi URL
    }
}
