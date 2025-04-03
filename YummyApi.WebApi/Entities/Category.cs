namespace YummyApi.WebApi.Entities
{
    public class Category
    {
        public int CategoryID { get; set; } //Kategori ID
        public string CategoryName { get; set; } //Kategori Adı
        public List<Product> Products { get; set; } //Ürünler böylelikle Kategori ile Ürünleri İlişkili hale getirdik
        //Bu kod sayesinde Product tablosuna CategoryID eklemiş Olduk
    }
}
