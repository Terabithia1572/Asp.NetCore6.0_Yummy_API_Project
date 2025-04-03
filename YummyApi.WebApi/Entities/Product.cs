namespace YummyApi.WebApi.Entities
{
    public class Product
    {
        public int ProductID { get; set; } //Ürün ID
        public string ProductName { get; set; } //Ürün Adı
        public string ProductDescription { get; set; } //Ürün Açıklaması
        public decimal ProductPrice { get; set; } // Ürün Fiyatı
        public string ProductImageURL { get; set; } //Ürün Resmi URL

        public int? CategoryID { get; set; } //? Boş geçilebilir anlamı katıyor.. Kategori ile Ürünleri İlişkili hale getirdik
        public Category Category { get; set; } //Ürün Kategorisi Category Entitesinde ise bu kodu gireceğiz public List<Product> Products { get; set; } //Ürün Kategorileri
                                               //diyelim ki bir ürünün kategorisi lazım bize önce ürünlere gidecek ürünler içinde kategoeriye gidecek ve kategori içinde KategoriName yani Kategori Adını alacağız
                                               //Bu kod sayesinde Product tablosuna CategoryID eklemiş Olduk bire çok ilişki oldu


    }
}
